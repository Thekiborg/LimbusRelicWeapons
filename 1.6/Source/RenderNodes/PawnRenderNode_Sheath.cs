
namespace LimbusWeapons
{
	public class PawnRenderNode_Sheath : PawnRenderNode
	{
		public PawnRenderNode_Sheath(Pawn pawn, PawnRenderNodeProperties props, PawnRenderTree tree) : base(pawn, props, tree)
		{
		}


		protected override IEnumerable<Graphic> GraphicsFor(Pawn pawn)
		{
			if (Props is not PawnRenderNodeProperties_Sheath propsSheath)
			{
				Log.Error("PawnRenderNodeWorker_Sheath requires PawnRenderNodeProperties_Sheath to work");
				yield break;
			}
			if (propsSheath.texPathUndrafted is not null)
				yield return GraphicDatabase.Get<Graphic_Multi>(propsSheath.texPathUndrafted, ShaderFor(pawn), Vector2.one, ColorFor(pawn));

			if (propsSheath.texPathDrafted is not null)
				yield return GraphicDatabase.Get<Graphic_Multi>(propsSheath.texPathDrafted, ShaderFor(pawn), Vector2.one, ColorFor(pawn));
		}
	}
}
