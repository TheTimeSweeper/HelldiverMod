using System;
using HellDiverMod.Modules;
using HellDiverMod.Survivors;
using UnityEngine.UIElements;

namespace HellDiverMod.Survivors.HellDiver
{
    public static class HellDiverTokens
    {
        public static void Init()
        {
            AddHellDiverTokens();

            ////uncomment this to spit out a lanuage file with all the above tokens that people can translate
            ////make sure you set Language.usingLanguageFolder and printingEnabled to true
            //Language.PrintOutput("Spy.txt");
            //todo guide
            ////refer to guide on how to build and distribute your mod with the proper folders
        }

        public static void AddHellDiverTokens()
        {
            #region HellDiver
            string prefix = HellDiverSurvivor.HELLDIVER_PREFIX;

            string desc = "HellDiver<color=#CCD3E0>" + Environment.NewLine + Environment.NewLine;
            desc = desc + "< ! > ." + Environment.NewLine + Environment.NewLine;
            desc = desc + "< ! > ." + Environment.NewLine + Environment.NewLine;
            desc = desc + "< ! > ." + Environment.NewLine + Environment.NewLine;
            desc = desc + "< ! > ." + Environment.NewLine + Environment.NewLine;

            string lore = "Insert goodguy lore here";
            string outro = "..and so he dived, diver dive dive.";
            string outroFailure = "..and so he vanished, not HellDivering all over the place.";

            Language.Add(prefix + "NAME", "Diver");
            Language.Add(prefix + "DESCRIPTION", desc);
            Language.Add(prefix + "SUBTITLE", "");
            Language.Add(prefix + "LORE", lore);
            Language.Add(prefix + "OUTRO_FLAVOR", outro);
            Language.Add(prefix + "OUTRO_FAILURE", outroFailure);

            #region Skins
            Language.Add(prefix + "MASTERY_SKIN_NAME", "Alternate");
            #endregion

            #region Passive
            Language.Add(prefix + "PASSIVE_NAME", "");
            Language.Add(prefix + "PASSIVE_DESCRIPTION", $"");
            #endregion

            #region Primary
            Language.Add(prefix + "PRIMARY_PISTOL_NAME", "");
            Language.Add(prefix + "PRIMARY_PISTOL_DESCRIPTION", $"");
            #endregion

            #region Secondary
            Language.Add(prefix + "SECONDARY_THROWGRENADE_NAME", "");
            Language.Add(prefix + "SECONDARY_THROWGRENADE_DESCRIPTION", $"");
            #endregion

            #region Utility 
            Language.Add(prefix + "UTILITY_DIVE_NAME", "");
            Language.Add(prefix + "UTILITY_DIVE_DESCRIPTION", $"");
            #endregion

            #region Special
            Language.Add(prefix + "SPECIAL_STRATEGEM_NAME", "");
            Language.Add(prefix + "SPECIAL_STRATEGEM_DESCRIPTION", $"");
            #endregion

            #region Achievements
            //Language.Add(Tokens.GetAchievementNameToken(HellDiverMasterAchievement.identifier), "HellDiver: Mastery");
            //Language.Add(Tokens.GetAchievementDescriptionToken(HellDiverMasterAchievement.identifier), "As HellDiver, beat the game or obliterate on Monsoon.");
            /*
            Language.Add(Tokens.GetAchievementNameToken(SpyUnlockAchievement.identifier), "Dressed to Kill");
            Language.Add(Tokens.GetAchievementDescriptionToken(SpyUnlockAchievement.identifier), "Get a Backstab.");
            */
            #endregion

            #endregion
        }
    }
}