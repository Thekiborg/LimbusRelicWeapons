namespace LimbusWeapons
{
	public class StartPart_EyeOfOdin : StatPart
	{
		public override string ExplanationPart(StatRequest req)
		{
			if (req.Thing is Pawn pawn && pawn.health.hediffSet.TryGetHediff(LimbusDefOfs.LRW_EyeOfOdin, out var hediff))
			{
				if (hediff.TryGetComp<HediffComp_EyeOfOdin>(out var comp))
				{
					return $"{hediff.LabelBase}: {comp.GetDodgeChance.ToStringPercent()}";
				}
			}
			return null;
		}

		public override void TransformValue(StatRequest req, ref float val)
		{
			if (req.Thing is Pawn pawn && pawn.health.hediffSet.TryGetHediff(LimbusDefOfs.LRW_EyeOfOdin, out var hediff))
			{
				if (hediff.TryGetComp<HediffComp_EyeOfOdin>(out var comp))
				{
					float dodgeChance = comp.GetDodgeChance;
					if (dodgeChance > 0.01)
					{
						val = comp.GetDodgeChance * 100;
					}
				}
			}
		}
	}
}
