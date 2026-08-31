namespace LimbusWeapons
{
	public class ThingClass_Bladetrail : Thing
	{
		private Pawn pawn;
		public Pawn Pawn
		{
			set => pawn = value;
		}
		private ModExtension ModExtension => field ??= def.GetModExtension<ModExtension>();


		public override void TickLong()
		{
			if ((Current.Game.tickManager.TicksGame - TickSpawned) / GenDate.TicksPerHour >= ModExtension.bladetrail.hoursExist)
			{
				Destroy();
			}
		}


		public override void Destroy(DestroyMode mode = DestroyMode.Vanish)
		{
			ModExtension.bladetrail.effecterOnDestroy.Spawn(this, Map);
			GameComponentLimbusWeapons.SpawnedBladetrailsByPawn[pawn].Remove(this);
			base.Destroy(mode);
		}


		public override void ExposeData()
		{
			base.ExposeData();
			Scribe_References.Look(ref pawn, "ThingClass_Bladetrail_Pawn");
		}
	}
}
