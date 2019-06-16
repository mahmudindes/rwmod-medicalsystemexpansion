using System.Collections.Generic;
using Verse;

namespace OrenoMSE
{
    public class HediffCompProperties_AdditionalHediff : HediffCompProperties
    {
        public HediffCompProperties_AdditionalHediff()
        {
            this.compClass = typeof(HediffComp_AdditionalHediff);
        }

        public List<HediffDef> hediffsToAdd = new List<HediffDef>();
    }
}
