using System.Reflection;
using Harmony;
using Verse;

namespace OrenoMSE.ModSupport
{
    [StaticConstructorOnStartup]
    public class MedicalSystemExpansion : Mod
    {
        public MedicalSystemExpansion(ModContentPack content) : base(content)
        {
            var harmony = HarmonyInstance.Create("OrenoMSE.ModSupport");
            harmony.PatchAll(Assembly.GetExecutingAssembly());
        }
    }
}
