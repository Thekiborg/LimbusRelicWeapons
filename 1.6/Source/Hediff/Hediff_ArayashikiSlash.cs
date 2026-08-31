using System.Linq;

namespace LimbusWeapons
{
	public class Hediff_ArayashikiSlash : Hediff_Injury
	{
		private ModExtension ModExt => field ??= def.GetModExtension<ModExtension>();
		private HediffComp_TendArayashiki TendComp => field ??= GetComp<HediffComp_TendArayashiki>();


		public override void Heal(float amount)
		{

		}


		public override void TickInterval(int delta)
		{
			base.TickInterval(delta);
			if (pawn.IsHashIntervalTick(2000, delta))
			{
				if (pawn.MapHeld.mapPawns.AllPawnsSpawned.Any(
					p => p?.equipment?.Primary?.def == ModExt?.arayashiki?.arayashikiSheathed
					|| p?.equipment?.Primary?.def == ModExt?.arayashiki?.arayashikiUnsheathed))
				{
					TendComp?.tendTicksLeft = -1;
					return;
				}


				if (pawn.Map.listerThings.AnyThingWithDef(ModExt?.arayashiki?.arayashikiUnsheathed)
					|| pawn.Map.listerThings.AnyThingWithDef(ModExt?.arayashiki?.arayashikiSheathed))
				{
					TendComp?.tendTicksLeft = -1;
				}
			}
		}


		public override bool TryMergeWith(Hediff other)
		{
			return false;
		}
	}
}
