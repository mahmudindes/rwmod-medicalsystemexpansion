using Harmony;
using Verse;

namespace OrenoMSE
{
    public class Harmony_PawnGenerator
    {
        [HarmonyPatch(typeof(PawnGenerator))]
        [HarmonyPatch("GenerateInitialHediffs")]
        public class PawnGenerator_GenerateInitialHediffs
        {
            [HarmonyPostfix]
            public static void InitialZeroSeverityComp(Pawn pawn)
            {
                foreach (Hediff hediff in pawn.health.hediffSet.hediffs)
                {
                    if (hediff.def.HasComp(typeof(HediffComp_InitialZeroSeverity)))
                    {
                        hediff.Severity = 0f;
                    }
                }
            }
        }
    }
}
