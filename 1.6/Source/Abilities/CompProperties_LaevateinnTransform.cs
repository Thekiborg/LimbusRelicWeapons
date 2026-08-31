namespace LimbusWeapons
{
	public class CompProperties_LaevateinnTransform : CompProperties_AbilityEffect
	{
		public CompProperties_LaevateinnTransform()
		{
			compClass = typeof(AbilityComp_LaevateinnTransform);
		}

		public float progressNeededForUse;
		public ThingDef nextLaevateinn;
	}
}
