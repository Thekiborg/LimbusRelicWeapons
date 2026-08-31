namespace LimbusWeapons
{
	public class PawnRenderNodeProperties_Sheath : PawnRenderNodeProperties
	{
		public PawnRenderNodeProperties_Sheath()
		{
			workerClass = typeof(PawnRenderNodeWorker_Sheath);
			nodeClass = typeof(PawnRenderNode_Sheath);
		}

		public float layerBehindBody;

		[NoTranslate]
		public string texPathDrafted;

		[NoTranslate]
		public string texPathUndrafted;
	}
}
