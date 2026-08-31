
namespace LimbusWeapons
{
	public class PawnRenderNodeWorker_Sheath : PawnRenderNodeWorker
	{
		public override float LayerFor(PawnRenderNode node, PawnDrawParms parms)
		{
			if (node.Props is not PawnRenderNodeProperties_Sheath props)
			{
				Log.Error("PawnRenderNodeWorker_Sheath requires PawnRenderNodeProperties_Sheath to work");
				return base.LayerFor(node, parms);
			}
			if (parms.pawn.Rotation != Rot4.North)
			{
				return props.layerBehindBody;
			}
			return base.LayerFor(node, parms);
		}


		public override bool CanDrawNow(PawnRenderNode node, PawnDrawParms parms)
		{
			if (node.Props is not PawnRenderNodeProperties_Sheath props)
			{
				Log.Error("PawnRenderNodeWorker_Sheath requires PawnRenderNodeProperties_Sheath to work");
				return base.CanDrawNow(node, parms);
			}
			if (props.texPathUndrafted is not null && !IsAttacking(parms.pawn) && !parms.pawn.Drafted)
			{
				return base.CanDrawNow(node, parms);
			}
			if (props.texPathDrafted is not null && (parms.pawn.Drafted || IsAttacking(parms.pawn)))
			{
				return base.CanDrawNow(node, parms);
			}
			return false;
		}


		protected override Graphic GetGraphic(PawnRenderNode node, PawnDrawParms parms)
		{
			if (node.Props is not PawnRenderNodeProperties_Sheath props)
			{
				Log.Error("PawnRenderNodeWorker_Sheath requires PawnRenderNodeProperties_Sheath to work");
				return base.GetGraphic(node, parms);
			}
			if (props.texPathUndrafted is not null && !IsAttacking(parms.pawn) && !parms.pawn.Drafted)
			{
				return node?.Graphics[0];
			}
			if (props.texPathDrafted is not null && (parms.pawn.Drafted || IsAttacking(parms.pawn)))
			{
				return node?.Graphics[1];
			}
			return base.GetGraphic(node, parms);
		}


		public override Quaternion RotationFor(PawnRenderNode node, PawnDrawParms parms)
		{
			float num = node.DebugAngleOffset;
			if (node.Props.drawData != null)
			{
				num += node.Props.drawData.RotationOffsetForRot(parms.facing);
			}
			if (!parms.flags.FlagSet(PawnRenderFlags.Portrait) && node.TryGetAnimationRotation(parms, out var offset))
			{
				num += offset;
			}
			return Quaternion.AngleAxis(num, Vector3.up);
		}


		private static bool IsAttacking(Pawn pawn)
		{
			return pawn.IsAttacking() || pawn.stances.curStance is Stance_Cooldown;
		}
	}
}
