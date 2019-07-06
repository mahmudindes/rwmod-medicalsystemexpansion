using Verse;

namespace OrenoMSE
{
    public class HediffCompProperties_PrettyLabel : HediffCompProperties
    {
        public HediffCompProperties_PrettyLabel()
        {
            this.compClass = typeof(HediffComp_PrettyLabel);
        }

        public string labelPretty;

        public string labelMale;

        public string labelFemale;

        public string labelGenderless;
    }
}
