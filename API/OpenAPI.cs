﻿using System.Net.Mime;
using VedAstro.Library;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.DurableTask;
using Microsoft.DurableTask.Client;
using Newtonsoft.Json.Linq;
using iText.Commons.Bouncycastle.Cert.Ocsp;
using Newtonsoft.Json;


namespace API
{
    public static class OpenAPI
    {


        /// <summary>
        /// https://api.vedastro.org/Location/Singapore/Time/23:59/31/12/2000/+08:00/Planet/Sun/Sign/
        /// </summary>
        [Function(nameof(Income1))]
        public static async Task<HttpResponseData> Income1([HttpTrigger(AuthorizationLevel.Anonymous,
                "get",
                Route = "Location/{locationName}/Time/{hhmmStr}/{dateStr}/{monthStr}/{yearStr}/{offsetStr}/{celestialBodyType}/{celestialBodyName}/{propertyName}")]
            HttpRequestData incomingRequest,
            [DurableClient] DurableTaskClient durableTaskClient,
            string locationName,
            string hhmmStr,
            string dateStr,
            string monthStr,
            string yearStr,
            string offsetStr,
            string celestialBodyType,
            string celestialBodyName,
            string propertyName)
        {
            //log the call
            APILogger.Visitor(incomingRequest);


            WebResult<GeoLocation>? geoLocationResult = await Tools.AddressToGeoLocation(locationName);
            var geoLocation = geoLocationResult.Payload;

            //clean time text
            var timeStr = $"{hhmmStr} {dateStr}/{monthStr}/{yearStr} {offsetStr}";
            var parsedTime = new Time(timeStr, geoLocation);


            //send to sorter
            return await FrontDeskSorter(celestialBodyType, celestialBodyName, propertyName, parsedTime, geoLocationResult, incomingRequest, durableTaskClient);
        }

        [Function(nameof(Income2))]
        public static async Task<HttpResponseData> Income2([HttpTrigger(AuthorizationLevel.Anonymous,
                "get",
                Route = "Location/{locationName}/Time/{hhmmStr}/{dateStr}/{monthStr}/{yearStr}/{offsetStr}/{celestialBodyType}/{celestialBodyName}")]
            HttpRequestData incomingRequest,
            [DurableClient] DurableTaskClient durableTaskClient,
            string locationName,
            string hhmmStr,
            string dateStr,
            string monthStr,
            string yearStr,
            string offsetStr,
            string celestialBodyType,
            string celestialBodyName)
        {
            //log the call
            APILogger.Visitor(incomingRequest);


            WebResult<GeoLocation>? geoLocationResult = await Tools.AddressToGeoLocation(locationName);
            var geoLocation = geoLocationResult.Payload;

            //clean time text
            var timeStr = $"{hhmmStr} {dateStr}/{monthStr}/{yearStr} {offsetStr}";
            var parsedTime = new Time(timeStr, geoLocation);

            //send to sorter (no property, set null)
            return await FrontDeskSorter(celestialBodyType, celestialBodyName, null, parsedTime, geoLocationResult, incomingRequest, durableTaskClient);
        }

        [Function(nameof(Income3))]
        public static async Task<HttpResponseData> Income3([HttpTrigger(AuthorizationLevel.Anonymous,
                "get",
                Route = "Location/{locationName}/Time/{hhmmStr}/{dateStr}/{monthStr}/{yearStr}/{offsetStr}/{celestialBodyType}")]
            HttpRequestData incomingRequest,
            [DurableClient] DurableTaskClient durableTaskClient,
            string locationName,
            string hhmmStr,
            string dateStr,
            string monthStr,
            string yearStr,
            string offsetStr,
            string celestialBodyType)
        {
            //log the call
            APILogger.Visitor(incomingRequest);


            WebResult<GeoLocation>? geoLocationResult = await Tools.AddressToGeoLocation(locationName);
            var geoLocation = geoLocationResult.Payload;

            //clean time text
            var timeStr = $"{hhmmStr} {dateStr}/{monthStr}/{yearStr} {offsetStr}";
            var parsedTime = new Time(timeStr, geoLocation);

            //send to sorter (no property, set null)
            return await FrontDeskSorter(celestialBodyType, "", null, parsedTime, geoLocationResult, incomingRequest, durableTaskClient);
        }



        private static async Task<HttpResponseData> FrontDeskSorter(string celestialBodyType, string celestialBodyName, string propertyName, Time parsedTime, WebResult<GeoLocation>? geoLocationResult,
            HttpRequestData incomingRequest, DurableTaskClient durableTaskClient)
        {

            var individualPropertySelected = !string.IsNullOrEmpty(propertyName);

            //all planet body calls
            if (celestialBodyType.ToLower() == "planet")
            {
                //get the planet data needed
                var planetNameResult = PlanetName.TryParse(celestialBodyName, out var planetName);
                //check result 1st before parsing
                if (!planetNameResult || !geoLocationResult.IsPass) { return APITools.FailMessage("Invalid Planet Name", incomingRequest); }

                //allows to dynamically select property that would other wise come together in list below
                if (individualPropertySelected)
                {
                    //find > execute > wrap to JSON matching function property
                    var result = Tools.ExecuteCalculatorByApiName<PlanetName, Time>(propertyName, planetName, parsedTime);

                    return APITools.PassMessageJson(result, incomingRequest);
                }
                //else get all
                else
                {
                    //all planet related data
                    //get all calculators that can accept a planet name and time
                    var planetTimeCalcs = Tools.ExecuteCalculatorByParam<PlanetName, Time>(planetName, parsedTime);

                    //send the payload on it's merry way
                    return APITools.PassMessageJson(planetTimeCalcs, incomingRequest);
                }

            }

            //all house body calls
            if (celestialBodyType.ToLower() == "house")
            {
                //get the house data needed
                var houseName = HouseNameExtensions.FromString(celestialBodyName);

                //check result 1st before parsing
                if ((houseName == null) || !geoLocationResult.IsPass) { return APITools.FailMessage("Invalid House Name", incomingRequest); }

                //allows to dynamically select property that would other wise come together in list below
                if (individualPropertySelected)
                {
                    //find > execute > wrap to JSON matching function property
                    var result = Tools.ExecuteCalculatorByApiName<HouseName, Time>(propertyName, (HouseName)houseName, parsedTime);

                    return APITools.PassMessageJson(result, incomingRequest);
                }
                //else get all
                else
                {
                    //all house related data
                    //get all calculators that can accept a house name and time
                    var houseTimeCalcs = Tools.ExecuteCalculatorByParam<HouseName, Time>((HouseName)houseName, parsedTime);

                    //send the payload on it's mary way
                    return APITools.PassMessageJson(houseTimeCalcs, incomingRequest);
                }

            }


            //sky chart
            var skyChartWidth = 750.0;
            var skyChartHeight = 230.0;

            //NOTE: each frame is cached
            if (celestialBodyType.ToLower() == "skychart")
            {
                //create unique id based on params to recognize future calls (caching)
                var callerId = $"{parsedTime.GetHashCode()}{skyChartWidth}{skyChartHeight}";

                Func<Task<string>> generateChart = () => SkyChartManager.GenerateChart(parsedTime, skyChartWidth, skyChartHeight);

                var chart = await APITools.CacheExecuteTask(generateChart, callerId);

                return APITools.SendSvgToCaller(chart, incomingRequest);

            }

            if (celestialBodyType.ToLower() == "skychartgif")
            {
                //create unique id based on params to recognize future calls (caching)
                var callerId = $"{parsedTime.GetHashCode()}{skyChartWidth}{skyChartHeight}GIF";

                //var chartGif = await SkyChartManager.GenerateChartGif(parsedTime, skyChartWidth, skyChartHeight);
                
                //squeeze the Sky Juice!
                var chartTask = () => SkyChartManager.GenerateChartGif(parsedTime, skyChartWidth, skyChartHeight);

                //get chart if in cache, else make and save in cache
                var chartGif = await APITools.CacheExecuteTask2(chartTask, callerId, MediaTypeNames.Image.Gif);

                return APITools.SendFileToCaller(chartGif, incomingRequest, MediaTypeNames.Image.Gif);

            }
            if (celestialBodyType.ToLower() == "skychartgifasync")
            {
                ////ready data needed to make chart
                //var chartSpecs = new JObject();
                //chartSpecs["Time"] = parsedTime.ToJson();
                //chartSpecs["Width"] = skyChartWidth;
                //chartSpecs["Height"] = skyChartHeight;
                //var chartSpecsStr = chartSpecs.ToString(Formatting.None); //convert to string else can't be parsed by JSON.NET

                ////create unique id based on params to recognize future calls (caching)
                //var callerId = $"{parsedTime.GetHashCode()}{skyChartWidth}{skyChartHeight}";


                ////only process new if no ready and valid cache
                ////note : done with durable for easy caching
                //await APITools.CallIfInvalid(durableTaskClient, nameof(SkyChartAPI.GetSkyChartGIFAsync), callerId, chartSpecsStr);


                ////wait for completion and return the gif in one call
                //var notComplete = true;
                //OrchestrationMetadata taskRef;
                //while (notComplete)
                //{
                //    //check if complete
                //    taskRef = await durableTaskClient.GetInstanceAsync(callerId);

                //    //mark if need to wait or result is ready
                //    notComplete = !(taskRef.RuntimeStatus == OrchestrationRuntimeStatus.Completed);

                //    //wait
                //    await Task.Delay(100);
                //}

                //taskRef = await durableTaskClient.GetInstanceAsync(callerId, true, CancellationToken.None);
                //var chart = taskRef.ReadOutputAs<byte[]>();


                //return APITools.SendGifToCaller(chart, incomingRequest);

            }


            return APITools.FailMessage("End Of ThE Line", incomingRequest);

            //-----------------------------


        }


        private static JObject GetSignDataJson(PlanetName planetName, Time parsedTime)
        {
            var planetSign = AstronomicalCalculator.GetPlanetRasiSign(planetName, parsedTime);
            var rootJson = new JObject();
            rootJson["Name"] = planetSign.GetSignName().ToString();
            rootJson["DegreesInSign"] = planetSign.GetDegreesInSign().TotalDegrees;

            return rootJson;
        }




    }
}
