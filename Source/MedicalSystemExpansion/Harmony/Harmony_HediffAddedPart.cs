using Harmony;
using Verse;

namespace OrenoMSE.Harmony
{
    public class Harmony_HediffAddedPart
    {
        [HarmonyPatch(typeof(Hediff_AddedPart))]
        [HarmonyPatch("PostAdd")]
        internal class HediffAddedPart_PostAdd
        {
            [HarmonyPostfix]
            [HarmonyPriority(Priority.Last)]
            private static void AdditionalHediff(Hediff_AddedPart __instance)
            {
                MSE_VanillaExtender.HediffApplyHediffs(__instance, __instance.pawn, __instance.Part);
            }
        }
    }
}
