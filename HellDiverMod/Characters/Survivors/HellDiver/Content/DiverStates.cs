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

            //Primary
            Modules.Content.AddEntityState(typeof(DiverFirePistol));

            //Secondary
            Modules.Content.AddEntityState(typeof(Dive));

            //Strategem
            Modules.Content.AddEntityState(typeof(StrategemM1Slot));
        }
    }
}
