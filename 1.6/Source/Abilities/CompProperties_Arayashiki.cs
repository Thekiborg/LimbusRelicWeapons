namespace LimbusWeapons
{
	public class CompProperties_Arayashiki : CompProperties_AbilityEffect
	{
		public CompProperties_Arayashiki()
		{
			compClass = typeof(AbilityComp_Arayashiki);
		}


		public ThingDef switchTo;
		public SoundDef sheathingSound;
	}
}
