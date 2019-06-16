using Harmony;
using Verse;

namespace OrenoMSE
{
    public class Harmony_Implant
    {
        [HarmonyPatch(typeof(Hediff_Implant))]
        [HarmonyPatch("PostAdd")]
        public class Implant_PostAdd
        {
            [HarmonyPostfix]
            public static void AdditionalHediffComp(ref Hediff_Implant __instance)
            {
                MSE_VanillaExtender.ApplyAdditionalHediffs(__instance.pawn, __instance.Part);
            }
        }
    }
}
