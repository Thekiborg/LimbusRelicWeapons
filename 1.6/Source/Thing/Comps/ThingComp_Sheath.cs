namespace LimbusWeapons
{
	internal class ThingComp_Sheath : ThingComp
	{
		public CompProperties_Sheath Props => (CompProperties_Sheath)props;


		public override void Notify_Equipped(Pawn pawn)
		{
			base.Notify_Equipped(pawn);
			if (UnityData.IsInMainThread)
			{
				pawn.Drawer?.renderer?.renderTree?.SetDirty();
			}
		}


		public override void Notify_Unequipped(Pawn pawn)
		{
			base.Notify_Unequipped(pawn);
			if (UnityData.IsInMainThread)
			{
				pawn.Drawer?.renderer?.renderTree?.SetDirty();
			}
		}


		public override List<PawnRenderNode> CompRenderNodes()
		{
			try
			{
				if (!Props.renderNodeProperties.NullOrEmpty() &&
					parent != null &&
					parent.ParentHolder != null &&
					parent.ParentHolder.ParentHolder is Pawn pawn)
				{
					List<PawnRenderNode> list = [];
					foreach (PawnRenderNodeProperties renderNodeProperty in Props.renderNodeProperties)
					{
						if (renderNodeProperty?.nodeClass != null && pawn.Drawer?.renderer?.renderTree != null)
						{
							try
							{
								PawnRenderNode node = (PawnRenderNode)Activator.CreateInstance(
									renderNodeProperty.nodeClass,
									pawn,
									renderNodeProperty,
									pawn.Drawer.renderer.renderTree
								);
								if (node != null)
								{
									list.Add(node);
								}
							}
							catch (Exception ex)
							{
								Log.Error($"Failed to create render node: {ex}");
							}
						}
					}
					return list;
				}
			}
			catch (Exception e)
			{
				Log.Error($"Error in CompRenderNodes: {e}");
			}

			return base.CompRenderNodes();
		}
	}
}
