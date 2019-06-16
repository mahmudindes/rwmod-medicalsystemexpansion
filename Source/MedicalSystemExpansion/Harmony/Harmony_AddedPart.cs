using Harmony;
using Verse;

namespace OrenoMSE
{
    public class Harmony_AddedPart
    {
        [HarmonyPatch(typeof(Hediff_AddedPart))]
        [HarmonyPatch("PostAdd")]
        public class AddedPart_PostAdd
        {
            [HarmonyPostfix]
            public static void AdditionalHediffComp(ref Hediff_AddedPart __instance)
            {
                MSE_VanillaExtender.ApplyAdditionalHediffs(__instance.pawn, __instance.Part);
            }
        }
    }
}
