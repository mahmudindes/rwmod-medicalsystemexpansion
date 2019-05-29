using System.Linq;
using Harmony;
using RimWorld;
using Verse;

namespace OrenoMSE.ModSupport
{
    public class BirdsAndBees
    {
        [HarmonyPatch(typeof(DefGenerator))]
        [HarmonyPatch("GenerateImpliedDefs_PreResolve")]
        public class GenerateImpliedDefs_PreResolve
        {
            [HarmonyPostfix]
            [HarmonyAfter(new string[] { "Fluffy.BirdsAndBees" })]
            public static void RemoveRecipeUsers()
            {
                var fleshRaces = DefDatabase<ThingDef>
                    .AllDefsListForReading
                    .Where(t => t.race?.IsFlesh ?? false);

                var humanoidRaces = fleshRaces.Where(td => td.race.Humanlike);

                if (ModsConfig.ActiveModsInLoadOrder.Any(mod => mod.Name == "The Birds and the Bees"))
                {
                    foreach (ThingDef race in humanoidRaces)
                    {
                        race.recipes.Remove(DefDatabase<RecipeDef>.GetNamed("InstallBasicReproductiveOrgans"));
                    }
                }
            }
        }
    }
}
