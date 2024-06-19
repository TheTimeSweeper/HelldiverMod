using HellDiverMod.Modules.BaseStates;
using System;
using System.Collections.Generic;
using System.Text;

namespace HellDiverMod.Survivors.HellDiver.SkillStates
{
    public class Dive : BaseHellDiverSkillState
    {

        public override void OnEnter()
        {
            base.OnEnter();
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();
            if(base.isAuthority && inputBank.skill3.justPressed || characterBody.isSprinting)
            {
                PlayCrossfade("Body", "DiveToStanding", "Dash.playbackRate", 0.5f, 0.01f);
                outer.SetNextStateToMain();
            }
        }
    }
}
