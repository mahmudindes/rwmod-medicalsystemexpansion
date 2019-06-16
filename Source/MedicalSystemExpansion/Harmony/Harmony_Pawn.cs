using System.Collections.Generic;
using Harmony;
using Verse;

namespace OrenoMSE
{
    public class Harmony_Pawn
    {
        [HarmonyPatch(typeof(Pawn))]
        [HarmonyPatch("GetGizmos")]
        public class Pawn_GetGizmos
        {
            [HarmonyPostfix]
            public static void VerbGiverExtended(ref Pawn __instance, IEnumerable<Gizmo> __result)
            {
                foreach (Hediff hediff in __instance.health.hediffSet.hediffs)
                {
                    HediffComp_VerbGiverExtended verbGiverExtended = hediff.TryGetComp<HediffComp_VerbGiverExtended>();
                    foreach (Gizmo k1 in verbGiverExtended.GetGizmos())
                    {
                        __result.Add(k1);
                    }
                }                
            }
        }
    }
}
