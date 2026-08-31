namespace LimbusWeapons
{
	public class HediffComp_EyeOfOdin : HediffComp
	{
		public HediffCompProperties_EyeOfOdin Props => (HediffCompProperties_EyeOfOdin)props;
		public float GetDodgeChance => Mathf.Lerp(1, 0, parent.Severity);
		public override string CompLabelInBracketsExtra => "EyeOfOdinBrackets".Translate(parent.Severity.ToStringPercent("F0").Named("PERCENTAGE"));


		public override void CompPostPostAdd(DamageInfo? dinfo)
		{
			base.CompPostPostAdd(dinfo);
			parent.Severity = parent.def.minSeverity;
		}


		public override void CompPostTickInterval(ref float severityAdjustment, int delta)
		{
			base.CompPostTickInterval(ref severityAdjustment, delta);
			if (parent.Severity >= 1)
			{
				Hediff eye = HediffMaker.MakeHediff(Props.overheatHediff, Pawn, parent.Part);
				Pawn.health.AddHediff(eye);
				Pawn.health.RemoveHediff(parent);
			}
			if (parent.pawn.IsHashIntervalTick(Props.reloadPeriod, delta))
			{
				parent.Severity -= 1 / Props.numberOfDodges;
			}
		}


		public void Dodged()
		{
			parent.Severity += 1 / Props.numberOfDodges;
		}
	}
}
