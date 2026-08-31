using System.Reflection;

namespace LimbusWeapons
{
	[HarmonyPatch]
	internal static class ProjectileCE_TryCollideWith_Prefix
	{
		private static MethodInfo TargetMethod()
		{
			// Resolve the method without loading the assembly directly
			return AccessTools.Method("CombatExtended.ProjectileCE:TryCollideWith");
		}


		// If false, the patch won't run
		public static bool Prepare()
		{
			return TargetMethod() is not null;
		}


		[HarmonyPrefix]
		internal static bool EyeOfOdinDodgeBulletsCECompat(Thing thing, ref bool __result)
		{
			if (thing is Pawn pawn)
			{
				if (pawn is null || pawn.health is null || pawn.health.hediffSet is null)
					return true;

				if (!pawn.health.hediffSet.TryGetHediff(LimbusDefOfs.LRW_EyeOfOdin, out var hediff))
					return true;

				if (!hediff.TryGetComp<HediffComp_EyeOfOdin>(out var comp))
					return true;


				if (Rand.Chance(comp.GetDodgeChance))
				{
					MoteMaker.ThrowText(pawn.Position.ToVector3(), pawn.Map, "TextMote_Dodge".Translate(), 1.9f);
					comp.Dodged();
					__result = false;
					return false;
				}
			}
			return true;
		}
	}
}
