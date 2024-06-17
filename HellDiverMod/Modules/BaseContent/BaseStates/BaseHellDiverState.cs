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
        protected HellDiverController HellDiverController;

        public override void OnEnter()
        {
            base.OnEnter();
        }
        public override void FixedUpdate()
        {
            base.FixedUpdate();
            RefreshState();
        }
        protected void RefreshState()
        {
            if (!HellDiverController)
            {
                HellDiverController = base.GetComponent<HellDiverController>();
            }
        }
    }
}
