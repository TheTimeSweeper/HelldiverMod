using EntityStates;
using RoR2;
using HellDiverMod.Survivors.HellDiver.Components;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine.Networking;

namespace HellDiverMod.Modules.BaseStates
{
    public abstract class BaseHellDiverSkillState : BaseSkillState
    {
        protected HellDiverController hellDiverController;
        public virtual void AddRecoil2(float x1, float x2, float y1, float y2)
        {
            this.AddRecoil(x1, x2, y1, y2);
        }
        public override void OnEnter()
        {
            RefreshState();
            base.OnEnter();
        }
        public override void FixedUpdate()
        {
            base.FixedUpdate();
        }
        protected void RefreshState()
        {
            if (!hellDiverController)
            {
                hellDiverController = base.GetComponent<HellDiverController>();
            }
        }
    }
}
