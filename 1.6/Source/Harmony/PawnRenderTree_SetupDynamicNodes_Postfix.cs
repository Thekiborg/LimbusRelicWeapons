using System.Reflection;

namespace LimbusWeapons
{
	[HarmonyPatch(typeof(PawnRenderTree), "SetupDynamicNodes")]
	internal static class PawnRenderTree_SetupDynamicNodes_Postfix
	{
		/*
		private static readonly Action<PawnRenderNode, PawnRenderNode> AddChild =
			(Action<PawnRenderNode, PawnRenderNode>)AccessTools.DeclaredMethod(typeof(PawnRenderTree), "AddChild")
				.CreateDelegate(typeof(Action<PawnRenderNode, PawnRenderNode>));
		*/

		private static readonly MethodInfo AddChild = AccessTools.DeclaredMethod(typeof(PawnRenderTree), "AddChild");

		[HarmonyPostfix]
		internal static void ReadRenderNodeFromWeapon(PawnRenderTree __instance, Pawn ___pawn)
		{
			ThingComp_Sheath comp = null;
			___pawn?.equipment?.Primary?.TryGetComp(out comp);
			if (comp is null || comp.Props.renderNodeProperties is null)
				return;

			var weaponRenderNodes = comp.CompRenderNodes();
			if (weaponRenderNodes.NullOrEmpty())
				return;

			foreach (PawnRenderNode node in weaponRenderNodes)
			{
				if (node is not null && __instance.ShouldAddNodeToTree(node.Props))
				{
					AddChild.Invoke(__instance, [node, null]);
				}
			}
		}
	}
}
