using R2API;
using HellDiverMod.Survivors.HellDiver.Components.UI;
using RoR2.Projectile;
using RoR2.UI;
using UnityEngine;
using UnityEngine.AddressableAssets;
using RoR2;
using UnityEngine.AI;
using UnityEngine.Networking;

namespace HellDiverMod.Survivors.HellDiver
{
    public class HellDiverAssets
    {
        public static GameObject stratagemProjectile;
        public static GameObject hellDiverUI;

        public static void Init(AssetBundle bundle)
        {
            stratagemProjectile = bundle.LoadAsset<GameObject>("StratagemProjectile");
            stratagemProjectile.GetComponent<ProjectileController>().ghostPrefab = UnityEngine.AddressableAssets.Addressables.LoadAssetAsync<GameObject>("RoR2/Base/Commando/CommandoGrenadeGhost.prefab").WaitForCompletion();

            hellDiverUI = bundle.LoadAsset<GameObject>("HellDiverUI");

            GameObject entry = bundle.LoadAsset<GameObject>("HellDiverUIStratagem").InstantiateClone("HellDiverUIStratagem", false);

            GameObject skillRoot = Addressables.LoadAssetAsync<GameObject>("RoR2/Base/UI/HUDSimple.prefab").WaitForCompletion();
            skillRoot = skillRoot.transform.Find("MainContainer/MainUIArea/SpringCanvas/BottomRightCluster/Scaler/Skill1Root").gameObject;

            skillRoot = Object.Instantiate(skillRoot, entry.transform.Find("SkillAnchor"));
            skillRoot.transform.SetParent(entry.transform.Find("SkillAnchor"));
            skillRoot.transform.localPosition = Vector3.zero;
            skillRoot.transform.localScale = Vector3.one;
            skillRoot.transform.localRotation = Quaternion.identity;

            Object.Destroy(skillRoot.transform.Find("SkillBackgroundPanel").gameObject);

            StratagemUIEntry entryComponent = entry.GetComponent<StratagemUIEntry>();
            entryComponent.skillIcon = skillRoot.GetComponent<SkillIcon>();
            hellDiverUI.GetComponent<HellDiverUI>().entryPrefab = entryComponent;
        }

        private static void CleanChildren(Transform startingTrans)
        {
            for (int num = startingTrans.childCount - 1; num >= 0; num--)
            {
                if (startingTrans.GetChild(num).childCount > 0)
                {
                    CleanChildren(startingTrans.GetChild(num));
                }
                Object.DestroyImmediate(startingTrans.GetChild(num).gameObject);
            }
        }

        private static void CreateMaterials()
        {
        }

        private static void CreateModels()
        {
        }
        #region effects
        private static void CreateEffects()
        {
        }

        #endregion

        #region projectiles
        private static void CreateProjectiles()
        {
        }
        #endregion

        #region sounds
        private static void CreateSounds()
        {
        }
        #endregion
    }
}