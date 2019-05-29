using System.Collections.Generic;
using System.Linq;
using Harmony;
using RimWorld;
using Verse;

namespace OrenoMSE
{
    public class InstallArtificialBodyPart
    {
        [HarmonyPatch(typeof(Recipe_InstallArtificialBodyPart))]
        [HarmonyPatch("GetPartsToApplyOn")]
        public class InstallArtificialBodyPart_GetPartsToApplyOn
        {
            [HarmonyPostfix]
            public static void ExcludePartSystem(ref IEnumerable<BodyPartRecord> __result, Pawn pawn, RecipeDef recipe)
            {
                List<BodyPartRecord> bpList = __result.ToList();
                for (int i = 0; i < recipe.appliedOnFixedBodyParts.Count; i++)
                {
                    BodyPartDef part = recipe.appliedOnFixedBodyParts[i];
                    for (int j = 0; j < bpList.Count; j++)
                    {
                        BodyPartRecord record = bpList[j];
                        if (record.def == part)
                        {
                            var check1 = pawn.health.hediffSet.hediffs.Any((Hediff d) => (d is Hediff_AddedPartSystem) && d.Part == record);
                            var check2 = pawn.health.hediffSet.hediffs.Any((Hediff d) => (d is Hediff_AddedPartModule) && d.Part == record);
                            var check3 = pawn.health.hediffSet.hediffs.Any((Hediff d) => (d is Hediff_BodyPartModule) && d.Part == record);
                            if (check1 || check2 || check3)
                            {
                                bpList.Remove(record);
                            }
                        }
                    }
                }

                __result = bpList.AsEnumerable();
            }
        }
    }
}
