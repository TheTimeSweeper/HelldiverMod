using HellDiverMod.Modules.BaseStates;
using HellDiverMod.HellDiver.SkillStates;
using HellDiverMod.Survivors.HellDiver.SkillStates;

namespace HellDiverMod.Survivors.HellDiver
{
    public static class HellDiverStates
    {
        public static void Init()
        {
            //Base
            Modules.Content.AddEntityState(typeof(BaseHellDiverSkillState));
            Modules.Content.AddEntityState(typeof(BaseHellDiverState));
            Modules.Content.AddEntityState(typeof(BaseMeleeAttack));
            Modules.Content.AddEntityState(typeof(MainState));
            
            //Reload
            Modules.Content.AddEntityState(typeof(Reload));
            Modules.Content.AddEntityState(typeof(EnterReload));
            Modules.Content.AddEntityState(typeof(ReloadRevolver));
            Modules.Content.AddEntityState(typeof(EnterReloadRevolver));
            //Primary
            Modules.Content.AddEntityState(typeof(DiverFirePistol));
            Modules.Content.AddEntityState(typeof(DiverFireAR));
            Modules.Content.AddEntityState(typeof(DiverFireShotgun));
            Modules.Content.AddEntityState(typeof(DiverFireRevolver));

            //Secondary
            Modules.Content.AddEntityState(typeof(KnifeThrow));

            // Utility
            Modules.Content.AddEntityState(typeof(StartDive));
            Modules.Content.AddEntityState(typeof(Dive));

            //Strategem
            Modules.Content.AddEntityState(typeof(StrategemM1Slot));
        }
    }
}
