using System.Collections.Generic;
using RimWorld;
using Verse;

namespace OrenoMSE
{
    public class Recipe_InstallNaturalPartSystem : Recipe_InstallNaturalBodyPart
    {
        public override void ApplyOnPawn(Pawn pawn, BodyPartRecord part, Pawn billDoer, List<Thing> ingredients, Bill bill)
        {
            if (billDoer != null)
            {
                if (base.CheckSurgeryFail(billDoer, pawn, ingredients, part, bill))
                {
                    return;
                }
                TaleRecorder.RecordTale(TaleDefOf.DidSurgery, new object[]
                {
                    billDoer,
                    pawn
                });
                MedicalRecipesUtility.RestorePartAndSpawnAllPreviousParts(pawn, part, billDoer.Position, billDoer.Map);
            }

            pawn.health.AddHediff(this.recipe.addsHediff, part, null, null);
        }
    }
}
