namespace LimbusWeapons
{
	public class HediffCompProperties_EyeOfOdinOverheated : HediffCompProperties
	{
		public HediffCompProperties_EyeOfOdinOverheated()
		{
			compClass = typeof(HediffComp_EyeOfOdinOverheated);
		}

		public HediffDef cooledHediff;
		public int timeForBurn;
	}
}
