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
            if(base.isAuthority && inputBank.skill4.justReleased)
            {
                PlayCrossfade("FullBody, Override", "BufferEmpty", 0.1f);
                outer.SetNextStateToMain();
            }
        }
    }
}
