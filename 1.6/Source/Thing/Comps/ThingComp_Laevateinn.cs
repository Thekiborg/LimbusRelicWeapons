namespace LimbusWeapons
{
	public class ThingComp_Laevateinn : ThingComp
	{
		private float progress = 0;
		private bool skipTick;


		public float Progress
		{
			get => progress;
			set => progress = Mathf.Clamp(value, 0f, 4f);
		}


		public CompProperties_Laevateinn Props => (CompProperties_Laevateinn)props;


		public void Attacked()
		{
			Progress += Props.progressPerAttack;
			skipTick = true;
		}


		public override void CompTickLong()
		{
			base.CompTickLong();
			if (skipTick)
			{
				skipTick = false;
				return;
			}
			Progress -= Props.progressDecayPerDay * (GenTicks.TickLongInterval / (float)GenDate.TicksPerDay);

			if (Progress < Props.severityToRevert)
			{
				Thing newLaev = ThingMaker.MakeThing(Props.previousLaevateinn);
				newLaev.TryGetComp<ThingComp_Laevateinn>()?.Progress = Progress;

				if (ParentHolder is Pawn_EquipmentTracker et)
				{
					et.pawn.equipment.Primary.Destroy();
					et.pawn.equipment.AddEquipment(newLaev as ThingWithComps);
				}
				else
				{
					var tempPos = parent.Position;
					var tempMap = parent.Map;
					parent.Destroy();
					GenSpawn.Spawn(newLaev, tempPos, tempMap);
				}
			}
		}


		public override void PostExposeData()
		{
			base.PostExposeData();
			Scribe_Values.Look(ref progress, "LimbusWeapons_progress");
			Scribe_Values.Look(ref skipTick, "LimbusWeapons_skipTick");
		}
	}
}
