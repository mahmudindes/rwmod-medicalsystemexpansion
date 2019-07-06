using System.Collections.Generic;
using Verse;

namespace OrenoMSE
{
    public class HediffComp_AdditionalHediff : HediffComp
    {
        public HediffCompProperties_AdditionalHediff Props
        {
            get
            {
                return (HediffCompProperties_AdditionalHediff)this.props;
            }
        }

        public List<HediffDef> GetHediffs
        {
            get
            {
                return this.Props.hediffsToAdd;
            }
        }
    }
}
