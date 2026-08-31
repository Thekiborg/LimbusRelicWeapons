namespace LimbusWeapons
{
	public class CompProperties_DihuiSheath : CompProperties_AbilityEffect
	{
		public CompProperties_DihuiSheath()
		{
			compClass = typeof(AbilityComp_DihuiSheath);
		}


		public ThingDef switchTo;
		public bool unsheating;
		public SoundDef sheathingSound;
		public float bladetrailDamage;
		public float bladetrailPen;
		public EffecterDef effecterRend;
		public SoundDef soundRend;
	}
}
