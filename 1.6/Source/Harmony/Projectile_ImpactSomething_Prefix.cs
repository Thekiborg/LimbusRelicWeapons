namespace LimbusWeapons
{
	[HarmonyPatch(typeof(Projectile), "ImpactSomething")]
	internal static class Projectile_ImpactSomething_Prefix
	{
		[HarmonyPrefix]
		internal static bool EyeOfOdinDodgeBullets(Projectile __instance)
		{
			if (__instance.usedTarget.Thing is Pawn pawn && __instance.def.projectile.explosionRadius == 0)
			{
				if (pawn is null || pawn.health is null || pawn.health.hediffSet is null)
					return true;

				if (!pawn.health.hediffSet.TryGetHediff(LimbusDefOfs.LRW_EyeOfOdin, out var hediff))
					return true;

				if (!hediff.TryGetComp<HediffComp_EyeOfOdin>(out var comp))
					return true;

				if (Rand.Chance(comp.GetDodgeChance))
				{
					__instance.Destroy();
					MoteMaker.ThrowText(pawn.Position.ToVector3(), pawn.Map, "TextMote_Dodge".Translate(), 1.9f);
					comp.Dodged();
					return false;
				}

			}
			return true;
		}
	}
}
