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
}
