using Harmony;
using Verse;

namespace OrenoMSE.Harmony
{
    public class Harmony_AddedPart
    {
        [HarmonyPatch(typeof(Hediff_AddedPart))]
        [HarmonyPatch("PostAdd")]
        public class AddedPart_PostAdd
        {
            [HarmonyPostfix]
            public static void AdditionalHediffComp(Hediff_AddedPart __instance)
            {
                MSE_VanillaExtender.ApplyAdditionalHediffs(__instance);
            }
        }
    }
}
