﻿namespace Genso.Astrology.Library
{
    /// <summary>
    /// Staticly typed name list of events,
    /// This is not the primarly list, not all events here will be checked, only events in the XML list are checked
    /// Not all events here have to have a method
    /// </summary>
    public enum EventName
    {
        EmptyEvent,
        //Lunar Day
        LunarDay1_1stBrightHalf,
        LunarDay2_2ndBrightHalf,
        LunarDay3_3rdBrightHalf,
        LunarDay4_4thBrightHalf,
        LunarDay5_5thBrightHalf,
        LunarDay6_6thBrightHalf,
        LunarDay7_7thBrightHalf,
        LunarDay8_8thBrightHalf,
        LunarDay9_9thBrightHalf,
        LunarDay10_10thBrightHalf,
        LunarDay11_11thBrightHalf,
        LunarDay12_12thBrightHalf,
        LunarDay13_13thBrightHalf,
        LunarDay14_14thBrightHalf,
        LunarDay15_FullMoon,
        LunarDay16_1stDarkHalf,
        LunarDay17_2ndDarkHalf,
        LunarDay18_3rdDarkHalf,
        LunarDay19_4thDarkHalf,
        LunarDay20_5thDarkHalf,
        LunarDay21_6thDarkHalf,
        LunarDay22_7thDarkHalf,
        LunarDay23_8thDarkHalf,
        LunarDay24_9thDarkHalf,
        LunarDay25_10thDarkHalf,
        LunarDay26_11thDarkHalf,
        LunarDay27_12thDarkHalf,
        LunarDay28_13thDarkHalf,
        LunarDay29_14thDarkHalf,
        LunarDay30_NewMoon,

        GoodTarabala,
        BadTarabala,
        GoodChandrabala,
        BadChandrabala,
        SiddhaYogaSunday,
        SiddhaYogaMonday,
        SiddhaYogaTuesday,
        SiddhaYogaWednesday,
        SiddhaYogaThursday,
        SiddhaYogaFriday,
        SiddhaYogaSaturday,
        AmritaSiddhaYoga,
        PanchangaSuddhi,
        UgraYoga,
        SuryaSankramana,
        CustomEvent,
        KarthariDosha,
        ShashtashtaRiphagathaChandraDosha,
        SagrahaChandraDosha,
        UdayasthaSuddhi,
        SiddhaYoga,
        SakunaKarana,
        BadNithyaYoga,
        LagnaThyajya,
        GoodPanchaka,
        BadPanchaka,
        BadTaraChandraPanchaka,
        GoodTaraChandraPanchaka,
        GoodTaraChandra,
        BadTaraChandra,
        BhriguShatka,
        Kujasthama,
        GoodHairCutting,
        GoodNailCutting,
        FixedConstellationRuling,
        SoftConstellationRuling,
        LightConstellationRuling,
        SharpConstellationRuling,
        MovableConstellationRuling,
        DreadfulConstellationRuling,
        MixedConstellationRuling,
        BadForStartingAllAgriculture,
        LagnaLordIsWeekdayLord,
        GoodLunarDayAgriculture,
        BadLagnaForAllAgriculture,
        GoodYogaForAllAgriculture,
        GoodAnySeedsSowing,
        Ekadashi,
        NotAuspiciousDay,
        GoodPlanetsInLagna,
        GoodForPlantingFloweringPlants,
        GoodForPlantingGarlic,
        GoodForPlantingPeachAndOthers,
        GoodForPlantingTomatoesAndOthers,
        GoodForPlantingGrains,
        GoodForPlantingOnionAndOthers,
        GoodForPlantingPepperAndOthers,
        GoodForPlantingPotatoAndOthers,
        GoodForPlantingGrainsAndOthers,
        GoodForPlantingPumpkinsAndOthers,
        GoodForPlantingTrees,
        GoodForPlantingFlowerSeeds,
        GoodForPlantingSugarcane,
        GoodForPlantingFruitTrees,
        GoodForPlantingFlowerTrees,
        GoodForPlantingFlowers,
        GoodForPlantingFlowerCuttings,
        GoodRullingConstellation,
        BadRullingConstellation,
        GoodTakingInjections,
        GoodSellingForProfit,
        BavaKarana,
        TaitulaKarana,
        BhadraKarana,
        JanmaNakshatraRulling,
        SunIsStrong,
        MoonIsStrong,
        MarsIsStrong,
        MercuryIsStrong,
        JupiterIsStrong,
        VenusIsStrong,
        SaturnIsStrong,
        House1IsStrong,
        House2IsStrong,
        House3IsStrong,
        House4IsStrong,
        House5IsStrong,
        House6IsStrong,
        House7IsStrong,
        House8IsStrong,
        House9IsStrong,
        House10IsStrong,
        House11IsStrong,
        House12IsStrong,
        BadForBuyingToolsUtensilsJewellery,
        GoodForBuyingBrassVessels,
        GoodForBuyingCopperVessels,
        GoodForBuyingSteelIronVessels,
        GoodForBuyingSilverVessels,
        GoodForBuyingJewellery,
        GoodPlanetsIn11th,
        GoodPlanetsInKendra,
        Sunrise,
        Sunset,
        Midday,
        TarabalaJanmaStrong,
        TarabalaSampatStrong,
        TarabalaVipatStrong,
        TarabalaKshemaStrong,
        TarabalaPratyakStrong,
        TarabalaSadhanaStrong,
        TarabalaNaidhanaStrong,
        TarabalaMitraStrong,
        TarabalaParamaMitraStrong,
        TarabalaJanmaMiddling,
        TarabalaSampatMiddling,
        TarabalaVipatMiddling,
        TarabalaKshemaMiddling,
        TarabalaPratyakMiddling,
        TarabalaSadhanaMiddling,
        TarabalaNaidhanaMiddling,
        TarabalaMitraMiddling,
        TarabalaParamaMitraMiddling,
        TarabalaJanmaWeak,
        TarabalaSampatWeak,
        TarabalaVipatWeak,
        TarabalaKshemaWeak,
        TarabalaPratyakWeak,
        TarabalaSadhanaWeak,
        TarabalaNaidhanaWeak,
        TarabalaMitraWeak,
        TarabalaParamaMitraWeak,
        Papashadvargas,
        House1LordInHouse1,
        House1LordInHouse2,
        House1LordInHouse3,
        House1LordInHouse4,
        House1LordInHouse5,
        House1LordInHouse6,
        House1LordInHouse7,
        House1LordInHouse8,
        House1LordInHouse9,
        House1LordInHouse10,
        House1LordInHouse11,
        House1LordInHouse12,
        House2LordInHouse1,
        House2LordInHouse2,
        House2LordInHouse3,
        House2LordInHouse4,
        House2LordInHouse5,
        House2LordInHouse6,
        House2LordInHouse7,
        House2LordInHouse8,
        House2LordInHouse9,
        House2LordInHouse10,
        House2LordInHouse11,
        House2LordInHouse12,
        House3LordInHouse1,
        House3LordInHouse2,
        House3LordInHouse3,
        House3LordInHouse4,
        House3LordInHouse5,
        House3LordInHouse6,
        House3LordInHouse7,
        House3LordInHouse8,
        House3LordInHouse9,
        House3LordInHouse10,
        House3LordInHouse11,
        House3LordInHouse12
    }
}