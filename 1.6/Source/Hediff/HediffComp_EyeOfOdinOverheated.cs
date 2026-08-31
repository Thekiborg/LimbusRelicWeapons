using System.Linq;

namespace LimbusWeapons
{
	public class HediffComp_EyeOfOdinOverheated : HediffComp
	{
		public HediffCompProperties_EyeOfOdinOverheated Props => (HediffCompProperties_EyeOfOdinOverheated)props;

		public override void CompPostPostRemoved()
		{
			base.CompPostPostRemoved();
			Hediff eye = HediffMaker.MakeHediff(Props.cooledHediff, Pawn, parent.Part);
			Pawn.health.AddHediff(eye);
		}


		public override void CompPostTickInterval(ref float severityAdjustment, int delta)
		{
			base.CompPostTickInterval(ref severityAdjustment, delta);
			if (parent.pawn.IsHashIntervalTick(Props.timeForBurn, delta))
			{
				var head = parent.Part.parent;
				var skull = head.parts.Where(bpr => bpr.def == LimbusDefOfs.Skull).First();
				Hediff burn = HediffMaker.MakeHediff(LimbusDefOfs.LRW_EyeSocketBurn, Pawn, skull);
				burn.Severity = 2f;
				Pawn.health.AddHediff(burn);
			}
		}
	}
}
