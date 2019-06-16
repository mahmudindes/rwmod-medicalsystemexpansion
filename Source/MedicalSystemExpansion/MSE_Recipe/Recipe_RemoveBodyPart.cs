using System;
using System.Collections.Generic;
using RimWorld;
using RimWorld.Planet;
using Verse;

namespace OrenoMSE
{
    public class Recipe_RemoveBodyPart : Recipe_Surgery
    {
        public override IEnumerable<BodyPartRecord> GetPartsToApplyOn(Pawn pawn, RecipeDef recipe)
        {
            IEnumerable<BodyPartRecord> parts = pawn.health.hediffSet.GetNotMissingParts(BodyPartHeight.Undefined, BodyPartDepth.Undefined, null, null);
            using (IEnumerator<BodyPartRecord> enumerator = parts.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    BodyPartRecord part = enumerator.Current;
                    var check1 = pawn.health.hediffSet.hediffs.Any((Hediff d) => !(d is Hediff_AddedPartSystem) && d.Part == part);
                    var check2 = pawn.health.hediffSet.hediffs.Any((Hediff d) => !(d is Hediff_AddedPartSystemNoModule) && d.Part == part);
                    if (pawn.health.hediffSet.HasDirectlyAddedPartFor(part) && (check1 || check2))
                    {
                        if (!pawn.health.hediffSet.AncestorHasDirectlyAddedParts(part))
                        {
                            yield return part;
                        }                        
                    }
                    else if (MedicalRecipesUtility.IsCleanAndDroppable(pawn, part) && !pawn.health.hediffSet.PartOrAnyAncestorHasDirectlyAddedParts(part))
                    {
                        yield return part;
                    }
                    else if (part != pawn.RaceProps.body.corePart && part.def.canSuggestAmputation && pawn.health.hediffSet.hediffs.Any((Hediff d) => !(d is Hediff_Injury) && d.def.isBad && d.Visible && d.Part == part))
                    {
                        yield return part;
                    }
                }
            }

            yield break;
        }

        public override bool IsViolationOnPawn(Pawn pawn, BodyPartRecord part, Faction billDoerFaction)
        {
            return pawn.Faction != billDoerFaction && pawn.Faction != null && HealthUtility.PartRemovalIntent(pawn, part) == BodyPartRemovalIntent.Harvest;
        }

        public override void ApplyOnPawn(Pawn pawn, BodyPartRecord part, Pawn billDoer, List<Thing> ingredients, Bill bill)
        {
            bool flag1 = MedicalRecipesUtility.IsClean(pawn, part);
            bool flag2 = this.IsViolationOnPawn(pawn, part, Faction.OfPlayer);

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
                MedicalRecipesUtility.SpawnNaturalPartIfClean(pawn, part, billDoer.Position, billDoer.Map);
                MedicalRecipesUtility.SpawnThingsFromHediffs(pawn, part, billDoer.Position, billDoer.Map);
            }

            DamageDef surgicalCut = DamageDefOf.SurgicalCut;
            float amount = 99999f;
            float armorPenetration = 999f;
            pawn.TakeDamage(new DamageInfo(surgicalCut, amount, armorPenetration, -1f, null, part, null, DamageInfo.SourceCategory.ThingOrUnknown, null));

            if (flag1)
            {
                if (pawn.Dead)
                {
                    ThoughtUtility.GiveThoughtsForPawnExecuted(pawn, PawnExecutionKind.OrganHarvesting);
                }
                ThoughtUtility.GiveThoughtsForPawnOrganHarvested(pawn);
            }

            if (flag2 && pawn.Faction != null && billDoer != null && billDoer.Faction != null)
            {
                Faction faction = pawn.Faction;
                Faction faction2 = billDoer.Faction;
                int goodwillChange = -15;
                string reason = "GoodwillChangedReason_RemovedBodyPart".Translate(part.LabelShort);
                GlobalTargetInfo? lookTarget = new GlobalTargetInfo?(pawn);
                faction.TryAffectGoodwillWith(faction2, goodwillChange, true, true, reason, lookTarget);
            }
        }

        public override string GetLabelWhenUsedOn(Pawn pawn, BodyPartRecord part)
        {
            if (pawn.RaceProps.IsMechanoid || pawn.health.hediffSet.PartOrAnyAncestorHasDirectlyAddedParts(part))
            {
                return RecipeDefOf.RemoveBodyPart.label;
            }

            BodyPartRemovalIntent bodyPartRemovalIntent = HealthUtility.PartRemovalIntent(pawn, part);
            if (bodyPartRemovalIntent != BodyPartRemovalIntent.Amputate)
            {
                if (bodyPartRemovalIntent != BodyPartRemovalIntent.Harvest)
                {
                    throw new InvalidOperationException();
                }

                return "HarvestOrgan".Translate();
            }
            else
            {
                if (part.depth == BodyPartDepth.Inside || part.def.socketed)
                {
                    return "RemoveOrgan".Translate();
                }

                return "Amputate".Translate();
            }
        }
    }
}
