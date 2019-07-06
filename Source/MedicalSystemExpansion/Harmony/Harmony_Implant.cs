using Harmony;
using Verse;

namespace OrenoMSE.Harmony
{
    public class Harmony_Implant
    {
        [HarmonyPatch(typeof(Hediff_Implant))]
        [HarmonyPatch("PostAdd")]
        public class Implant_PostAdd
        {
            [HarmonyPostfix]
            public static void AdditionalHediffComp(Hediff_Implant __instance)
            {
                MSE_VanillaExtender.ApplyAdditionalHediffs(__instance);
            }
        }
    }
}
