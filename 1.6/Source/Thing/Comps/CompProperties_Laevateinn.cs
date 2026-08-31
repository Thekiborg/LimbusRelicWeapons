namespace LimbusWeapons
{
	public class CompProperties_Laevateinn : CompProperties
	{
		public CompProperties_Laevateinn()
		{
			compClass = typeof(ThingComp_Laevateinn);
		}


		public float progressPerAttack;
		public float progressDecayPerDay;
		public ThingDef previousLaevateinn;
		public float severityToRevert = -1;
	}
}
