using EntityStates;
using RoR2;
using HellDiverMod.Survivors.HellDiver.Components;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine.Networking;

namespace HellDiverMod.Modules.BaseStates
{
    public abstract class BaseHellDiverState : BaseState
    {
        protected HellDiverController hellDiverController;

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
