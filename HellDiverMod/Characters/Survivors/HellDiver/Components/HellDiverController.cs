using R2API;
using RoR2;
using RoR2.HudOverlay;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Networking;
using System;

namespace HellDiverMod.Survivors.HellDiver.Components
{
    public class HellDiverController : MonoBehaviour
    {
        private CharacterBody characterBody;
        private ModelSkinController skinController;
        private ChildLocator childLocator;
        private CharacterModel characterModel;
        private Animator animator;
        private SkillLocator skillLocator;

        private string primaryGunString;
        private string currentGunAnim;

        public float stageReload = 0;

        public void Awake()
        {
            this.characterBody = this.GetComponent<CharacterBody>();
            ModelLocator modelLocator = this.GetComponent<ModelLocator>();
            this.childLocator = modelLocator.modelBaseTransform.GetComponentInChildren<ChildLocator>();
            this.animator = modelLocator.modelBaseTransform.GetComponentInChildren<Animator>();
            this.characterModel = modelLocator.modelBaseTransform.GetComponentInChildren<CharacterModel>();
            this.skillLocator = this.GetComponent<SkillLocator>();
            this.skinController = modelLocator.modelTransform.gameObject.GetComponent<ModelSkinController>();

            this.Invoke("InitialGunString", 0.3f);
        }

        public void FixedUpdate()
        {

        }
        public void InitialGunString()
        {
            if (HellDiverSurvivor.HELLDIVER_PREFIX + "PRIMARY_PISTOL_NAME" == skillLocator.primary.skillNameToken)
            {
                this.animator.SetLayerWeight(this.animator.GetLayerIndex("Body"), 1f);
                this.childLocator.FindChild("PistolModel").gameObject.SetActive(true);
                primaryGunString = "PistolModel";
            }
            else if(HellDiverSurvivor.HELLDIVER_PREFIX + "PRIMARY_ASSAULT_RIFLE_NAME" == skillLocator.primary.skillNameToken)
            {
                this.animator.SetLayerWeight(this.animator.GetLayerIndex("Body, AR"), 1f);
                this.childLocator.FindChild("ARModel").gameObject.SetActive(true);
                primaryGunString = "ARModel";
                currentGunAnim = "Body, AR";
            }
            else if (HellDiverSurvivor.HELLDIVER_PREFIX + "PRIMARY_SHOTGUN_NAME" == skillLocator.primary.skillNameToken)
            {
                this.animator.SetLayerWeight(this.animator.GetLayerIndex("Body, Shotgun"), 1f);
                this.childLocator.FindChild("ShotgunModel").gameObject.SetActive(true);
                primaryGunString = "ShotgunModel";
                currentGunAnim = "Body, Shotgun";
            }
            else if (HellDiverSurvivor.HELLDIVER_PREFIX + "PRIMARY_REVOLVER_NAME" == skillLocator.primary.skillNameToken)
            {
                this.animator.SetLayerWeight(this.animator.GetLayerIndex("Body"), 1f);
                this.childLocator.FindChild("RevolverModel").gameObject.SetActive(true);
                primaryGunString = "RevovlerModel";
            }
        }

        public void EquipGunString(string gunString, string gunAnimString)
        {
            if(primaryGunString != gunString) 
            {
                this.animator.SetLayerWeight(this.animator.GetLayerIndex(currentGunAnim), 0f);
                this.animator.SetLayerWeight(this.animator.GetLayerIndex(gunAnimString), 1f);
                currentGunAnim = gunAnimString;
                this.childLocator.FindChild(primaryGunString).gameObject.SetActive(false);
                this.childLocator.FindChild(gunString).gameObject.SetActive(true);
            }
        }
    }
}
