namespace LimbusWeapons
{
	public class CompProperties_Sheath : CompProperties
	{
		public CompProperties_Sheath()
		{
			compClass = typeof(ThingComp_Sheath);
		}


		public List<PawnRenderNodeProperties> renderNodeProperties;
	}
}
