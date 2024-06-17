using RoR2;
using RoR2.Skills;
using UnityEngine;

namespace HellDiverMod.Survivors.HellDiver.Components
{
    public class HellDiverPassive : MonoBehaviour
    {
        public SkillDef HellDiverPassiveSkillDef;

        public GenericSkill passiveSkillSlot;

        public bool isDive
        {
            get
            {
                if (HellDiverPassiveSkillDef && passiveSkillSlot)
                {
                    return passiveSkillSlot.skillDef == HellDiverPassiveSkillDef;
                }

                return false;
            }
        }
    }
}