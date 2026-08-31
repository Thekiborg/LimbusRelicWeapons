namespace LimbusWeapons
{
	public class Verb_MeleeAttackBladetrail : Verb_MeleeAttackDamage
	{
		protected override DamageWorker.DamageResult ApplyMeleeDamageToTarget(LocalTargetInfo target)
		{
			var result = base.ApplyMeleeDamageToTarget(target);
			if (target.Thing is not null)
				SpawnBladetrail(target.Thing);
			return result;
		}


		private void SpawnBladetrail(Thing target)
		{
			if (target.MapHeld is null) return;

			ThingClass_Bladetrail trail = ThingMaker.MakeThing(GetBladetrailSize()) as ThingClass_Bladetrail;
			trail.Pawn = CasterPawn;
			trail.Rotation = Rot4.Random;

			if (!GameComponentLimbusWeapons.SpawnedBladetrailsByPawn.ContainsKey(CasterPawn))
			{
				GameComponentLimbusWeapons.SpawnedBladetrailsByPawn.Add(CasterPawn, []);
			}
			GameComponentLimbusWeapons.SpawnedBladetrailsByPawn[CasterPawn] ??= [];
			GameComponentLimbusWeapons.SpawnedBladetrailsByPawn[CasterPawn].Add(trail);

			GenSpawn.Spawn(trail, target.Position, target.MapHeld);
		}


		private static ThingDef GetBladetrailSize()
		{
			if (Rand.Chance(0.3f))
				return LimbusDefOfs.LRW_BladetrailBig;
			if (Rand.Chance(0.3f))
				return LimbusDefOfs.LRW_BladetrailMedium;
			return LimbusDefOfs.LRW_BladetrailSmall;
		}
	}
}
