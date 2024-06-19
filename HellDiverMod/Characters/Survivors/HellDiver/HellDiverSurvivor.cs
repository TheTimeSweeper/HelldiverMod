using BepInEx.Configuration;
using EntityStates;
using EntityStates.Engi.Mine;
using HellDiverMod.General.Components;
using HellDiverMod.HellDiver.SkillStates;
using HellDiverMod.Modules;
using HellDiverMod.Modules.Characters;
using HellDiverMod.Survivors.HellDiver.Components;
using HellDiverMod.Survivors.HellDiver.Components.UI;
using HellDiverMod.Survivors.HellDiver.SkillDefs;
using HellDiverMod.Survivors.HellDiver.SkillStates;
using RoR2;
using RoR2.Projectile;
using RoR2.Skills;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HellDiverMod.Survivors.HellDiver
{
    public class HellDiverSurvivor : SurvivorBase<HellDiverSurvivor>
    {
        public override string assetBundleName => "helldiverbundle";

        public override string bodyName => "DiverBody";
        
        public override string masterName => "HellDiverMonsterMaster";

        public override string modelPrefabName => "mdlHellDiver";
        public override string displayPrefabName => "HellDiverDisplay";

        public const string HELLDIVER_PREFIX = HellDiverPlugin.DEVELOPER_PREFIX + "_HELLDIVER_";

        public static SkillDef strategemSkillDef1;
        public static SkillDef strategemSkillDef2;
        public static SkillDef strategemSkillDef3;
        public static SkillDef strategemSkillDef4;

        public override string survivorTokenPrefix => HELLDIVER_PREFIX;

        internal static GameObject characterPrefab;


        public override BodyInfo bodyInfo => new BodyInfo
        {
            bodyName = bodyName,
            bodyNameToken = HELLDIVER_PREFIX + "NAME",
            subtitleNameToken = HELLDIVER_PREFIX + "SUBTITLE",

            characterPortrait = assetBundle.LoadAsset<Texture>("texDiverIcon"),
            bodyColor = Color.blue,
            sortPosition = 69.6f,

            //crosshairBundlePath = "GICrosshair",
            crosshairAddressablePath = "RoR2/Base/UI/StandardCrosshair.prefab",
            podPrefab = HellDiverAssets.hellDiverPodPrefab,

            maxHealth = 60f,
            healthGrowth = 60f * 0.15f,
            shield = 20f,
            shieldGrowth = 20f * 0.15f,
            armor = -10f,

            jumpCount = 1,
        };
        public override CustomRendererInfo[] customRendererInfos => new CustomRendererInfo[]

{               new CustomRendererInfo
                {
                    childName = "ArcThrowerModel",
                },
                new CustomRendererInfo
                {
                    childName = "ArmorModel",
                },
                new CustomRendererInfo
                {
                    childName = "BazookaModel",
                },
                new CustomRendererInfo
                {
                    childName = "BodyModel",
                },
                new CustomRendererInfo
                {
                    childName = "CapeModel",
                },
                new CustomRendererInfo
                {
                    childName = "DeagleModel",
                },
                new CustomRendererInfo
                {
                    childName = "HeavyMGModel",
                },
                new CustomRendererInfo
                {
                    childName = "JapaneseBloodFlowsThroughMyVeinsHatModel",
                },
                new CustomRendererInfo
                {
                    childName = "KnifeModel",
                },
                new CustomRendererInfo
                {
                    childName = "KatanaModel",
                },
                new CustomRendererInfo
                {
                    childName = "ShotgunModel",
                },
                new CustomRendererInfo
                {
                    childName = "PistolModel",
                },
                new CustomRendererInfo
                {
                    childName = "RailgunModel",
                },
                new CustomRendererInfo
                {
                    childName = "RevolverModel",
                },
                new CustomRendererInfo
                {
                    childName = "RocketLauncherModel",
                },
                new CustomRendererInfo
                {
                    childName = "SyringeModel",
                },

};
        public override UnlockableDef characterUnlockableDef => null; // GIUnlockables.characterUnlockableDef;

        public override ItemDisplaysBase itemDisplays { get; } = new HellDiverMod.General.JoeItemDisplays();

        public static SkillDef throwStratagemSkillDef;

        public override void Initialize()
        {
            //if (!General.GeneralConfig.GIEnabled.Value)
            //    return;

            base.Initialize();
        }

        public override void InitializeCharacter()
        {
            //need the character unlockable before you initialize the survivordef
            //GIUnlockables.Init();

            base.InitializeCharacter();
        }

        public override void OnCharacterInitialized()
        {
            //Config.ConfigureBody(prefabCharacterBody, GIConfig.SectionBody);

            //GIConfig.Init();

            HellDiverStates.Init();
            HellDiverTokens.Init();

            //GIBuffs.Init(assetBundle);

            InitializeEntityStateMachines();
            InitializeSkills();
            InitializeSkins();
            InitializeCharacterMaster();

            AdditionalBodySetup();

            characterPrefab = bodyPrefab;

            AddHooks();
        }
        
        private void AdditionalBodySetup()
        {
            //AddHitboxes();
            bodyPrefab.AddComponent<StratagemInputController>();
            bodyPrefab.AddComponent<HellDiverController>();
        }

        public override void InitializeEntityStateMachines() 
        {
            //clear existing state machines from your cloned body (probably commando)
            //omit all this if you want to just keep theirs
            Prefabs.ClearEntityStateMachines(bodyPrefab);

            //if you set up a custom main characterstate, set it up here
                //don't forget to register custom entitystates in your HenryStates.cs
            //the main "body" state machine has some special properties
            Prefabs.AddMainEntityStateMachine(bodyPrefab, "Body", typeof(MainState), typeof(EntityStates.SpawnTeleporterState));
            
            Prefabs.AddEntityStateMachine(bodyPrefab, "Weapon");
            Prefabs.AddEntityStateMachine(bodyPrefab, "Weapon2");
            Prefabs.AddEntityStateMachine(bodyPrefab, "Dive");
        }

        #region skills
        public override void InitializeSkills()
        {
            bodyPrefab.AddComponent<HellDiverPassive>();
            Skills.CreateSkillFamilies(bodyPrefab);

            AddPassiveSkills();
            AddPrimarySkills();
            AddSecondarySkills();
            AddUtiitySkills();
            AddSpecialSkills();
            AddStratagemSkills();

            throwStratagemSkillDef = Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = "HellDiverThrowStratagem",
                skillNameToken = HELLDIVER_PREFIX + "OVERRIDE_THROW_NAME",
                skillDescriptionToken = HELLDIVER_PREFIX + "OVERRIDE_THROW_DESCRIPTION",
                //keywordTokens = new string[] { "KEYWORD_STUNNING" },
                skillIcon = assetBundle.LoadAsset<Sprite>("texIconChronoPrimary"),

                activationState = new EntityStates.SerializableEntityStateType(typeof(ThrowStratagem)),
                activationStateMachineName = "Weapon",
                interruptPriority = EntityStates.InterruptPriority.Any,

                baseRechargeInterval = 0f,
                baseMaxStock = 1,

                rechargeStock = 1,
                requiredStock = 0,
                stockToConsume = 0,

                resetCooldownTimerOnUse = false,
                fullRestockOnAssign = false,
                dontAllowPastMaxStocks = false,
                mustKeyPress = false,
                beginSkillCooldownOnSkillEnd = false,

                isCombatSkill = true,
                canceledFromSprinting = false,
                cancelSprintingOnActivation = true,
                forceSprintDuringState = false,
            });

            strategemSkillDef1 = Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = "Strategem1",
                skillNameToken = HELLDIVER_PREFIX + "PRIMARY_STRATEGEM1_NAME",
                skillDescriptionToken = HELLDIVER_PREFIX + "PRIMARY_STRATEGEM1_DESCRIPTION",
                keywordTokens = new string[] { },
                skillIcon = assetBundle.LoadAsset<Sprite>("texConvictScepter"),

                activationState = new EntityStates.SerializableEntityStateType(typeof(StrategemM1Slot)),
                activationStateMachineName = "Weapon",
                baseMaxStock = 1,
                baseRechargeInterval = 0f,
                beginSkillCooldownOnSkillEnd = false,
                canceledFromSprinting = false,
                forceSprintDuringState = false,
                fullRestockOnAssign = true,
                interruptPriority = InterruptPriority.PrioritySkill,
                resetCooldownTimerOnUse = false,
                isCombatSkill = false,
                mustKeyPress = true,
                cancelSprintingOnActivation = false,
                rechargeStock = 1,
                requiredStock = 1,
                stockToConsume = 0
            });

            strategemSkillDef2 = Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = "Strategem2",
                skillNameToken = HELLDIVER_PREFIX + "PRIMARY_STRATEGEM2_NAME",
                skillDescriptionToken = HELLDIVER_PREFIX + "PRIMARY_STRATEGEM2_DESCRIPTION",
                keywordTokens = new string[] { },
                skillIcon = assetBundle.LoadAsset<Sprite>("texConvictScepter"),

                activationState = new EntityStates.SerializableEntityStateType(typeof(StrategemM1Slot)),
                activationStateMachineName = "Weapon",
                baseMaxStock = 1,
                baseRechargeInterval = 0f,
                beginSkillCooldownOnSkillEnd = false,
                canceledFromSprinting = false,
                forceSprintDuringState = false,
                fullRestockOnAssign = true,
                interruptPriority = InterruptPriority.PrioritySkill,
                resetCooldownTimerOnUse = false,
                isCombatSkill = false,
                mustKeyPress = true,
                cancelSprintingOnActivation = false,
                rechargeStock = 1,
                requiredStock = 1,
                stockToConsume = 0
            });

            strategemSkillDef3 = Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = "Strategem3",
                skillNameToken = HELLDIVER_PREFIX + "PRIMARY_STRATEGEM3_NAME",
                skillDescriptionToken = HELLDIVER_PREFIX + "PRIMARY_STRATEGEM3_DESCRIPTION",
                keywordTokens = new string[] { },
                skillIcon = assetBundle.LoadAsset<Sprite>("texConvictScepter"),

                activationState = new EntityStates.SerializableEntityStateType(typeof(StrategemM1Slot)),
                activationStateMachineName = "Weapon",
                baseMaxStock = 1,
                baseRechargeInterval = 0f,
                beginSkillCooldownOnSkillEnd = false,
                canceledFromSprinting = false,
                forceSprintDuringState = false,
                fullRestockOnAssign = true,
                interruptPriority = InterruptPriority.PrioritySkill,
                resetCooldownTimerOnUse = false,
                isCombatSkill = false,
                mustKeyPress = true,
                cancelSprintingOnActivation = false,
                rechargeStock = 1,
                requiredStock = 1,
                stockToConsume = 0
            });

            strategemSkillDef4 = Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = "Strategem4",
                skillNameToken = HELLDIVER_PREFIX + "PRIMARY_STRATEGEM4_NAME",
                skillDescriptionToken = HELLDIVER_PREFIX + "PRIMARY_STRATEGEM4_DESCRIPTION",
                keywordTokens = new string[] { },
                skillIcon = assetBundle.LoadAsset<Sprite>("texConvictScepter"),

                activationState = new EntityStates.SerializableEntityStateType(typeof(StrategemM1Slot)),
                activationStateMachineName = "Weapon",
                baseMaxStock = 1,
                baseRechargeInterval = 0f,
                beginSkillCooldownOnSkillEnd = false,
                canceledFromSprinting = false,
                forceSprintDuringState = false,
                fullRestockOnAssign = true,
                interruptPriority = InterruptPriority.PrioritySkill,
                resetCooldownTimerOnUse = false,
                isCombatSkill = false,
                mustKeyPress = true,
                cancelSprintingOnActivation = false,
                rechargeStock = 1,
                requiredStock = 1,
                stockToConsume = 0
            });



            SkillLocator skillLocator = bodyPrefab.GetComponent<SkillLocator>();

            skillLocator.passiveSkill.enabled = false;
        }
        private void AddPassiveSkills()
        {
            HellDiverPassive passive = bodyPrefab.GetComponent<HellDiverPassive>();

            passive.HellDiverPassiveSkillDef = Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = HELLDIVER_PREFIX + "PASSIVE_NAME",
                skillNameToken = HELLDIVER_PREFIX + "PASSIVE_NAME",
                skillDescriptionToken = HELLDIVER_PREFIX + "PASSIVE_DESCRIPTION",
                skillIcon = assetBundle.LoadAsset<Sprite>("texHellDiverPassive"),
                keywordTokens = new string[] { },
                activationState = new EntityStates.SerializableEntityStateType(typeof(EntityStates.Idle)),
                activationStateMachineName = "",
                baseMaxStock = 1,
                baseRechargeInterval = 0f,
                beginSkillCooldownOnSkillEnd = false,
                canceledFromSprinting = false,
                forceSprintDuringState = false,
                fullRestockOnAssign = true,
                interruptPriority = EntityStates.InterruptPriority.Any,
                resetCooldownTimerOnUse = false,
                isCombatSkill = false,
                mustKeyPress = false,
                cancelSprintingOnActivation = false,
                rechargeStock = 1,
                requiredStock = 2,
                stockToConsume = 1
            });

            Skills.AddPassiveSkills(passive.passiveSkillSlot.skillFamily, passive.HellDiverPassiveSkillDef);
        }
        private void AddPrimarySkills()
        {
            SkillDef pistolDef = Skills.CreateSkillDef(new SkillDefInfo
                (
                    "HellDiverPistol",
                    HELLDIVER_PREFIX + "PRIMARY_PISTOL_NAME",
                    HELLDIVER_PREFIX + "PRIMARY_PISTOL_DESCRIPTION",
                    assetBundle.LoadAsset<Sprite>("texPrimaryIcon"),
                    new EntityStates.SerializableEntityStateType(typeof(DiverFirePistol)),
                    "Weapon",
                    true,
                    true
                ));

            ReloadSkillDef arSkillDef = Skills.CreateReloadSkillDef(new ReloadSkillDefInfo
            {
                skillName = "HellDiverAR",
                skillNameToken = HELLDIVER_PREFIX + "PRIMARY_ASSAULT_RIFLE_NAME",
                skillDescriptionToken = HELLDIVER_PREFIX + "PRIMARY_ASSAULT_RIFLE_DESCRIPTION",
                keywordTokens = new string[] { },
                skillIcon = assetBundle.LoadAsset<Sprite>("texSecondaryIcon"),

                activationState = new EntityStates.SerializableEntityStateType(typeof(DiverFireAR)),
                activationStateMachineName = "Weapon",
                interruptPriority = EntityStates.InterruptPriority.Skill,

                baseRechargeInterval = 0f,
                baseMaxStock = 30,

                rechargeStock = 0,
                requiredStock = 1,
                stockToConsume = 1,

                resetCooldownTimerOnUse = false,
                fullRestockOnAssign = true,
                dontAllowPastMaxStocks = false,
                mustKeyPress = false,
                beginSkillCooldownOnSkillEnd = false,

                isCombatSkill = true,
                canceledFromSprinting = false,
                cancelSprintingOnActivation = true,
                forceSprintDuringState = false,

                graceDuration = 1.5f,
                reloadState = new EntityStates.SerializableEntityStateType(typeof(EnterReload)),
                reloadInterruptPriority = InterruptPriority.Any,

            });

            Skills.AddPrimarySkills(bodyPrefab, pistolDef, arSkillDef);
        }

        private void AddSecondarySkills()
        {
            //here is a basic skill def with all fields accounted for
            SkillDef secondarySkillDef1 = Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = "HellDiverGrenade",
                skillNameToken = HELLDIVER_PREFIX + "SECONDARY_THROWGRENADE_NAME",
                skillDescriptionToken = HELLDIVER_PREFIX + "SECONDARY_THROWGRENADE_DESCRIPTION",
                keywordTokens = new string[] { "KEYWORD_STUNNING" },
                skillIcon = assetBundle.LoadAsset<Sprite>("texSecondaryIcon"),
                
                activationState = new EntityStates.SerializableEntityStateType(typeof(EntityStates.Commando.CommandoWeapon.ThrowGrenade)),
                activationStateMachineName = "Weapon2",
                interruptPriority = EntityStates.InterruptPriority.Skill,

                baseRechargeInterval = 5f,
                baseMaxStock = 1,

                rechargeStock = 1,
                requiredStock = 1,
                stockToConsume = 1,

                resetCooldownTimerOnUse = false,
                fullRestockOnAssign = true,
                dontAllowPastMaxStocks = false,
                mustKeyPress = false,
                beginSkillCooldownOnSkillEnd = false,

                isCombatSkill = true,
                canceledFromSprinting = false,
                cancelSprintingOnActivation = true,
                forceSprintDuringState = false,

            });
            SkillDef knifeDef = Skills.CreateSkillDef(new SkillDefInfo
            {
                skillName = "HellDiverKnifeThrow",
                skillNameToken = HELLDIVER_PREFIX + "SECONDARY_KNIFETHROW_NAME",
                skillDescriptionToken = HELLDIVER_PREFIX + "SECONDARY_KNIFETHROW_DESCRIPTION",
                keywordTokens = new string[] { },
                skillIcon = assetBundle.LoadAsset<Sprite>("texSecondaryIcon"),

                activationState = new EntityStates.SerializableEntityStateType(typeof(KnifeThrow)),
                activationStateMachineName = "Weapon2",
                interruptPriority = EntityStates.InterruptPriority.Skill,

                baseRechargeInterval = 5f,
                baseMaxStock = 1,

                rechargeStock = 1,
                requiredStock = 1,
                stockToConsume = 1,

                resetCooldownTimerOnUse = false,
                fullRestockOnAssign = true,
                dontAllowPastMaxStocks = false,
                mustKeyPress = false,
                beginSkillCooldownOnSkillEnd = false,

                isCombatSkill = true,
                canceledFromSprinting = false,
                cancelSprintingOnActivation = false,
                forceSprintDuringState = false,

            });

            Skills.AddSecondarySkills(bodyPrefab, secondarySkillDef1, knifeDef);
        }

        private void AddUtiitySkills()
        {
            //here's a skilldef of a typical movement skill.
            SkillDefInfo skillDefInfo = new SkillDefInfo
            {
                skillName = "HellDive",
                skillNameToken = HELLDIVER_PREFIX + "UTILITY_DIVE_NAME",
                skillDescriptionToken = HELLDIVER_PREFIX + "UTILITY_DIVE_DESCRIPTION",
                skillIcon = assetBundle.LoadAsset<Sprite>("texUtilityIcon"),

                activationState = new EntityStates.SerializableEntityStateType(typeof(StartDive)),
                activationStateMachineName = "Dive",
                interruptPriority = EntityStates.InterruptPriority.Skill,

                baseRechargeInterval = 6f,
                baseMaxStock = 1,

                rechargeStock = 1,
                requiredStock = 1,
                stockToConsume = 1,

                resetCooldownTimerOnUse = false,
                fullRestockOnAssign = false,
                dontAllowPastMaxStocks = false,
                mustKeyPress = false,
                beginSkillCooldownOnSkillEnd = false,

                isCombatSkill = false,
                canceledFromSprinting = false,
                cancelSprintingOnActivation = false,
                forceSprintDuringState = true,
            };
            var utilitySkillDef1 = Skills.CreateSkillDef(skillDefInfo);

            Skills.AddUtilitySkills(bodyPrefab, utilitySkillDef1);
        }

        private void AddSpecialSkills()
        {
            StratagemComponentSkillDef specialSkillDef1 = Skills.CreateSkillDef<StratagemComponentSkillDef>(new SkillDefInfo
            {
                skillName = "HellDiverStratagem",
                skillNameToken = HELLDIVER_PREFIX + "SPECIAL_STRATEGEM_NAME",
                skillDescriptionToken = HELLDIVER_PREFIX + "SPECIAL_STRATEGEM_DESCRIPTION",
                skillIcon = assetBundle.LoadAsset<Sprite>("texSpecialIcon"),

                activationState = new EntityStates.SerializableEntityStateType(typeof(InputStratagem)),
                activationStateMachineName = "Weapon",
                interruptPriority = EntityStates.InterruptPriority.PrioritySkill,
                 
                baseMaxStock = 1,
                baseRechargeInterval = 0f,

                rechargeStock = 1,
                requiredStock = 0,
                stockToConsume = 0,

                isCombatSkill = false,
                mustKeyPress = true,
                fullRestockOnAssign = false,
                beginSkillCooldownOnSkillEnd = true,
            });

            Skills.AddSpecialSkills(bodyPrefab, specialSkillDef1);
        }

        private void AddStratagemSkills()
        {
            StratagemSkillDef OrbitalPrecisionStrike = Skills.CreateSkillDef<StratagemSkillDef>(new SkillDefInfo
            {
                skillName = "HellDiverOrbital1",
                skillNameToken = HELLDIVER_PREFIX + "STRATAGEM_ORBITAL1_NAME",
                skillDescriptionToken = HELLDIVER_PREFIX + "STRATAGEM_ORBITAL1_DESCRIPTION",
                //keywordTokens = new string[] { "KEYWORD_STUNNING" },
                skillIcon = assetBundle.LoadAsset<Sprite>("texSecondaryIcon"),

                activationState = new EntityStates.SerializableEntityStateType(typeof(BootlegOrbitalPrecisionStrike)),
                activationStateMachineName = "Weapon",
                interruptPriority = EntityStates.InterruptPriority.Skill,

                baseRechargeInterval = 5f,
                baseMaxStock = 1,

                rechargeStock = 1,
                requiredStock = 1,
                stockToConsume = 1,

                resetCooldownTimerOnUse = false,
                fullRestockOnAssign = false,
                dontAllowPastMaxStocks = false,
                mustKeyPress = false,
                beginSkillCooldownOnSkillEnd = false,

                isCombatSkill = true,
                canceledFromSprinting = false,
                cancelSprintingOnActivation = true,
                forceSprintDuringState = false,
            });
            OrbitalPrecisionStrike.sequence = new StratagemInput[] { 
                StratagemInput.UP, 
                StratagemInput.RIGHT, 
                StratagemInput.RIGHT 
            };

            StratagemSkillDef OrbitalPrecisionStrike2 = Skills.CreateSkillDef<StratagemSkillDef>(new SkillDefInfo
            {
                skillName = "HellDiverOrbital2",
                skillNameToken = HELLDIVER_PREFIX + "STRATAGEM_ORBITAL2_NAME",
                skillDescriptionToken = HELLDIVER_PREFIX + "STRATAGEM_ORBITAL2_DESCRIPTION",
                //keywordTokens = new string[] { "KEYWORD_STUNNING" },
                skillIcon = assetBundle.LoadAsset<Sprite>("texSecondaryIcon"),

                activationState = new EntityStates.SerializableEntityStateType(typeof(BootlegOrbitalPrecisionStrike)),
                activationStateMachineName = "Weapon",
                interruptPriority = EntityStates.InterruptPriority.Skill,

                baseRechargeInterval = 5f,
                baseMaxStock = 1,

                rechargeStock = 1,
                requiredStock = 1,
                stockToConsume = 1,

                resetCooldownTimerOnUse = false,
                fullRestockOnAssign = false,
                dontAllowPastMaxStocks = false,
                mustKeyPress = false,
                beginSkillCooldownOnSkillEnd = false,

                isCombatSkill = true,
                canceledFromSprinting = false,
                cancelSprintingOnActivation = true,
                forceSprintDuringState = false,
            });
            OrbitalPrecisionStrike2.sequence = new StratagemInput[] { 
                StratagemInput.UP, 
                StratagemInput.RIGHT, 
                StratagemInput.LEFT 
            };

            StratagemSkillDef OrbitalPrecisionStrike3 = Skills.CreateSkillDef<StratagemSkillDef>(new SkillDefInfo
            {
                skillName = "HellDiverOrbital3",
                skillNameToken = HELLDIVER_PREFIX + "STRATAGEM_ORBITAL3_NAME",
                skillDescriptionToken = HELLDIVER_PREFIX + "STRATAGEM_ORBITAL3_DESCRIPTION",
                //keywordTokens = new string[] { "KEYWORD_STUNNING" },
                skillIcon = assetBundle.LoadAsset<Sprite>("texSecondaryIcon"),

                activationState = new EntityStates.SerializableEntityStateType(typeof(BootlegOrbitalPrecisionStrike)),
                activationStateMachineName = "Weapon",
                interruptPriority = EntityStates.InterruptPriority.Skill,

                baseRechargeInterval = 5f,
                baseMaxStock = 1,

                rechargeStock = 1,
                requiredStock = 1,
                stockToConsume = 1,

                resetCooldownTimerOnUse = false,
                fullRestockOnAssign = false,
                dontAllowPastMaxStocks = false,
                mustKeyPress = false,
                beginSkillCooldownOnSkillEnd = false,

                isCombatSkill = true,
                canceledFromSprinting = false,
                cancelSprintingOnActivation = true,
                forceSprintDuringState = false,
            });
            OrbitalPrecisionStrike3.sequence = new StratagemInput[] { 
                StratagemInput.UP, 
                StratagemInput.RIGHT, 
                StratagemInput.DOWN, 
                StratagemInput.RIGHT 
            };

            StratagemSkillDef OrbitalPrecisionStrike4 = Skills.CreateSkillDef<StratagemSkillDef>(new SkillDefInfo
            {
                skillName = "HellDiverOrbital4",
                skillNameToken = HELLDIVER_PREFIX + "STRATAGEM_ORBITAL4_NAME",
                skillDescriptionToken = HELLDIVER_PREFIX + "STRATAGEM_ORBITAL4_DESCRIPTION",
                //keywordTokens = new string[] { "KEYWORD_STUNNING" },
                skillIcon = assetBundle.LoadAsset<Sprite>("texSecondaryIcon"),

                activationState = new EntityStates.SerializableEntityStateType(typeof(BootlegOrbitalPrecisionStrike)),
                activationStateMachineName = "Weapon",
                interruptPriority = EntityStates.InterruptPriority.Skill,

                baseRechargeInterval = 5f,
                baseMaxStock = 1,

                rechargeStock = 1,
                requiredStock = 1,
                stockToConsume = 1,

                resetCooldownTimerOnUse = false,
                fullRestockOnAssign = false,
                dontAllowPastMaxStocks = false,
                mustKeyPress = false,
                beginSkillCooldownOnSkillEnd = false,

                isCombatSkill = true,
                canceledFromSprinting = false,
                cancelSprintingOnActivation = true,
                forceSprintDuringState = false,
            });
            OrbitalPrecisionStrike4.sequence = new StratagemInput[] { 
                StratagemInput.UP, 
                StratagemInput.RIGHT, 
                StratagemInput.UP, 
                StratagemInput.LEFT 
            };
            
            for (int i = 0; i < 4; i++)
            {
                SkillFamily family = Skills.CreateSkillFamily(bodyPrefab + "StratagemFamily" + i);

                Skills.AddSkillsToFamily(family, OrbitalPrecisionStrike, OrbitalPrecisionStrike2, OrbitalPrecisionStrike3, OrbitalPrecisionStrike4);

                Skills.CreateGenericSkillWithSkillFamily(bodyPrefab, "Stratagem" + i, family, true);
            }
        }
        #endregion skills

        #region skins
        public override void InitializeSkins()
        {
            ModelSkinController skinController = prefabCharacterModel.gameObject.GetComponent<ModelSkinController>();
            ChildLocator childLocator = prefabCharacterModel.GetComponent<ChildLocator>();

            CharacterModel.RendererInfo[] defaultRendererinfos = prefabCharacterModel.baseRendererInfos;

            List<SkinDef> skins = new List<SkinDef>();

            #region DefaultSkin
            //this creates a SkinDef with all default fields
            SkinDef defaultSkin = Skins.CreateSkinDef("DEFAULT_SKIN",
                assetBundle.LoadAsset<Sprite>("texMainSkin"),
                defaultRendererinfos,
                prefabCharacterModel.gameObject);

            //these are your Mesh Replacements. The order here is based on your CustomRendererInfos from earlier
            //pass in meshes as they are named in your assetbundle
            //currently not needed as with only 1 skin they will simply take the default meshes
            //uncomment this when you have another skin
            //defaultSkin.meshReplacements = Modules.Skins.getMeshReplacements(assetBundle, defaultRendererinfos,
            //    "meshHenrySword",
            //    "meshHenryGun",
            //    "meshHenry");

            //add new skindef to our list of skindefs. this is what we'll be passing to the SkinController
            skins.Add(defaultSkin);
            #endregion

            //uncomment this when you have a mastery skin
            #region MasterySkin

            ////creating a new skindef as we did before
            //SkinDef masterySkin = Modules.Skins.CreateSkinDef(HENRY_PREFIX + "MASTERY_SKIN_NAME",
            //    assetBundle.LoadAsset<Sprite>("texMasteryAchievement"),
            //    defaultRendererinfos,
            //    prefabCharacterModel.gameObject,
            //    HenryUnlockables.masterySkinUnlockableDef);

            ////adding the mesh replacements as above. 
            ////if you don't want to replace the mesh (for example, you only want to replace the material), pass in null so the order is preserved
            //masterySkin.meshReplacements = Modules.Skins.getMeshReplacements(assetBundle, defaultRendererinfos,
            //    "meshHenrySwordAlt",
            //    null,//no gun mesh replacement. use same gun mesh
            //    "meshHenryAlt");

            ////masterySkin has a new set of RendererInfos (based on default rendererinfos)
            ////you can simply access the RendererInfos' materials and set them to the new materials for your skin.
            //masterySkin.rendererInfos[0].defaultMaterial = assetBundle.LoadMaterial("matHenryAlt");
            //masterySkin.rendererInfos[1].defaultMaterial = assetBundle.LoadMaterial("matHenryAlt");
            //masterySkin.rendererInfos[2].defaultMaterial = assetBundle.LoadMaterial("matHenryAlt");

            ////here's a barebones example of using gameobjectactivations that could probably be streamlined or rewritten entirely, truthfully, but it works
            //masterySkin.gameObjectActivations = new SkinDef.GameObjectActivation[]
            //{
            //    new SkinDef.GameObjectActivation
            //    {
            //        gameObject = childLocator.FindChildGameObject("GunModel"),
            //        shouldActivate = false,
            //    }
            //};
            ////simply find an object on your child locator you want to activate/deactivate and set if you want to activate/deacitvate it with this skin

            //skins.Add(masterySkin);

            #endregion
            
            skinController.skins = skins.ToArray();
        }
        #endregion skins

        //Character Master is what governs the AI of your character when it is not controlled by a player (artifact of vengeance, goobo)
        public override void InitializeCharacterMaster()
        {
            //you must only do one of these. adding duplicate masters breaks the game.

            //if you're lazy or prototyping you can simply copy the AI of a different character to be used
            Modules.Prefabs.CloneDopplegangerMaster(bodyPrefab, masterName, "Merc");

            //how to set up AI in code
            //GIAI.Init(bodyPrefab, masterName);

            //how to load a master set up in unity, can be an empty gameobject with just AISkillDriver components
            //assetBundle.LoadMaster(bodyPrefab, masterName);
        }

        private void AddHooks()
        {
            On.RoR2.UI.HUD.Awake += HUD_Awake;
            On.RoR2.Stage.RespawnCharacter += Stage_RespawnCharacter;
        }

        private void Stage_RespawnCharacter(On.RoR2.Stage.orig_RespawnCharacter orig, Stage self, CharacterMaster characterMaster)
        {
            bool firstStage = false;
            if(self.usePod == true) firstStage = true;
            if(characterMaster && characterMaster.bodyPrefab == HellDiverSurvivor.characterPrefab && !firstStage)
            {
                Transform playerSpawnTransform = self.GetPlayerSpawnTransform();
                Vector3 vector = Vector3.zero;
                Quaternion quaternion = Quaternion.identity;
                if (playerSpawnTransform)
                {
                    vector = playerSpawnTransform.position;
                    quaternion = playerSpawnTransform.rotation;
                }
                characterMaster.Respawn(vector, quaternion);
                if (characterMaster.GetComponent<PlayerCharacterMasterController>())
                {
                    self.spawnedAnyPlayer = true;
                }
                Run.instance.HandlePlayerFirstEntryAnimation(characterMaster.GetBody(), vector, quaternion);
            }
            else
            {
                orig.Invoke(self, characterMaster);
            }
        }
        private void HUD_Awake(On.RoR2.UI.HUD.orig_Awake orig, RoR2.UI.HUD self)
        {
            orig(self);
            self.gameObject.AddComponent<HellDiverHUDManager>();
        }
    }
}