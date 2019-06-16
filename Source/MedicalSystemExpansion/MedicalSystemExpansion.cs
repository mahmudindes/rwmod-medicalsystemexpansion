using System.Collections.Generic;
using System.Reflection;
using Harmony;
using Verse;

namespace OrenoMSE
{
    [StaticConstructorOnStartup]
    public class MedicalSystemExpansion
    {
        static MedicalSystemExpansion()
        {
            var harmony = HarmonyInstance.Create("OrenoMSE");
            harmony.PatchAll(Assembly.GetExecutingAssembly());
        }
    }

    public static class MSE_VanillaExtender
    {
        public static void ApplyAdditionalHediffs(Pawn pawn, BodyPartRecord part)
        {
            List<HediffDef> hediffs = new List<HediffDef>();
            foreach (Hediff hediff in pawn.health.hediffSet.hediffs)
            {
                if (hediff.Part == part)
                {
                    HediffCompProperties_AdditionalHediff additionalHediff = hediff.def.CompProps<HediffCompProperties_AdditionalHediff>();
                    if (additionalHediff != null)
                    {
                        hediffs = additionalHediff.hediffsToAdd;
                    }
                }
            }

            if (hediffs != null)
            {
                for (int i = 0; i < hediffs.Count; i++)
                {
                    pawn.health.AddHediff(hediffs[i], part, null, null);
                }
            }
        }
    }
}
