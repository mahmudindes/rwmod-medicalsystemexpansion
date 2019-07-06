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
        public static void ApplyAdditionalHediffs(Hediff hediff)
        {
            HediffComp_AdditionalHediff additionalHediffs = hediff.TryGetComp<HediffComp_AdditionalHediff>();
            if (additionalHediffs != null)
            {
                var hediffsToAdd = additionalHediffs.GetHediffs;
                var hediffToAdd = hediffsToAdd.Count;
                for (int i = 0; i < hediffToAdd; i++)
                {
                    hediff.pawn.health.AddHediff(hediffsToAdd[i], hediff.Part, null, null);
                }
            }
        }

        public static string PrettyLabel(Hediff hediff)
        {
            HediffComp_PrettyLabel prettyLabel = hediff.TryGetComp<HediffComp_PrettyLabel>();

            if (prettyLabel != null && !hediff.pawn.Dead)
            {
                string labelGendered = "unknown";
                string labelGenderless = prettyLabel.Props.labelGenderless;
                string labelMale = prettyLabel.Props.labelMale;
                string labelFemale = prettyLabel.Props.labelFemale;
                if (labelGenderless != null && hediff.pawn.gender == Gender.None)
                {
                    labelGendered = labelGenderless;
                }
                else if (labelMale != null && hediff.pawn.gender == Gender.Male)
                {
                    labelGendered = labelMale;
                }
                else if (labelFemale != null && hediff.pawn.gender == Gender.Female)
                {
                    labelGendered = labelFemale;
                }

                string label = string.Format(prettyLabel.Props.labelPretty, hediff.Part.customLabel, labelGendered);
                return label;
            }

            return hediff.def.label;
        }
    }
}
