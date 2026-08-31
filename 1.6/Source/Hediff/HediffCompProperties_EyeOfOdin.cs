namespace LimbusWeapons
{
	public class HediffCompProperties_EyeOfOdin : HediffCompProperties
	{
		public HediffCompProperties_EyeOfOdin()
		{
			compClass = typeof(HediffComp_EyeOfOdin);
		}


		public float numberOfDodges;
		public HediffDef overheatHediff;
		public int reloadPeriod;
	}
}
