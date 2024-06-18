using EntityStates;
using HellDiverMod.Modules.BaseStates;
using System;
using System.Collections.Generic;
using System.Text;

namespace HellDiverMod.Survivors.HellDiver.SkillStates
{
    public class EnterReload : BaseHellDiverState
    {
        public static float baseDuration = 0.1f;

        private float duration => baseDuration / attackSpeedStat;

        public override void OnEnter()
        {
            base.OnEnter();
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();
            if (base.isAuthority && base.fixedAge >= duration)
            {
                Log.Debug("relaoding!!!!");
                outer.SetNextState(new Reload());
            }
        }

        public override InterruptPriority GetMinimumInterruptPriority()
        {
            return InterruptPriority.Skill;
        }
    }
}
