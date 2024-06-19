using EntityStates;
using HellDiverMod.Modules.BaseStates;
using RoR2;
using System;
using System.Collections.Generic;
using System.Text;

namespace HellDiverMod.Survivors.HellDiver.SkillStates
{
    public class Reload : BaseHellDiverState
    {
        public static float baseDuration = 1.4f;
        private float duration;
        private float startReload;
        private bool StartReloadPlayed = false;
        private float magInserted;
        private bool magInsertedPlayed = false;
        private bool dontPlay = false;
        private bool hasGivenStock = false;

        public override void OnEnter()
        {
            RefreshState();
            base.OnEnter();
            this.duration = baseDuration / attackSpeedStat;
            if (this.hellDiverController.stageReload > 0f) this.duration = this.hellDiverController.stageReload;
            else this.hellDiverController.stageReload = this.duration;
            this.startReload = 0.04f * duration;
            this.magInserted = 0.6f * duration;
            base.PlayCrossfade("Gesture, Override", "ReloadAR", this.duration); // DO THIS LATER
        }
        public override void FixedUpdate()
        {
            base.FixedUpdate();
           
            if(base.fixedAge >= startReload && !StartReloadPlayed)
            {
                this.hellDiverController.stageReload = duration - startReload;
                StartReloadPlayed = true;
                Util.PlayAttackSpeedSound("Play_bandit2_m1_reload_bullet", base.gameObject, attackSpeedStat);
            }

            if (base.fixedAge >= magInserted && !magInsertedPlayed)
            {
                this.hellDiverController.stageReload = duration - magInserted;
                magInsertedPlayed = true;
                Util.PlayAttackSpeedSound("Play_bandit2_m1_reload_finish", base.gameObject, attackSpeedStat);
            }

            if (base.isAuthority && base.fixedAge >= this.duration)
            {
                GiveStock();
                this.hellDiverController.stageReload = 0f;
                this.outer.SetNextStateToMain();
            }
        }
        
        private void GiveStock()
        {
            if (!hasGivenStock)
            {
                for(int i = base.skillLocator.primary.stock; i < base.skillLocator.primary.maxStock; i++) base.skillLocator.primary.AddOneStock();
                hasGivenStock = true;
            }
        }

        public override InterruptPriority GetMinimumInterruptPriority()
        {
            return InterruptPriority.Skill;
        }

    }
    
}
