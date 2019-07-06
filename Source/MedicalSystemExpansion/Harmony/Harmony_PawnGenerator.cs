using Harmony;
using Verse;

namespace OrenoMSE.Harmony
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
                var hediffs = pawn.health.hediffSet.hediffs;
                var hediff = hediffs.Count;
                for (int i = 0; i < hediff; i++)
                {
                    if (hediffs[i].def.HasComp(typeof(HediffComp_InitialZeroSeverity)))
                    {
                        hediffs[i].Severity = 0f;
                    }
                }
            }
        }
    }
}
