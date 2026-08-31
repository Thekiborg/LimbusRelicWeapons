using Verse.Sound;

namespace LimbusWeapons
{
	public class Verb_MoonlitAzureBlade : Verb_MeleeAttackDamage
	{
		private const float AngleHorizontal = 52f;


		private ModExtension ModExt => field ??= CasterPawn.equipment.Primary.def.GetModExtension<ModExtension>();


		public override float? AimAngleOverride
		{
			get
			{
				float num = 0f;
				Stance_Busy stance_Busy = CasterPawn.stances?.curStance as Stance_Busy;
				if (stance_Busy is not null)
				{
					Vector3 vector = stance_Busy.focusTarg.HasThing ? stance_Busy.focusTarg.Thing.DrawPos : stance_Busy.focusTarg.Cell.ToVector3Shifted();
					if ((vector - CasterPawn.DrawPos).MagnitudeHorizontalSquared() > 0.001f)
					{
						num = (vector - CasterPawn.DrawPos).AngleFlat();
						if (num < 270f)
							num += AngleHorizontal;
						else
							num -= AngleHorizontal;
					}
				}
				return num;
			}
		}


		protected override DamageWorker.DamageResult ApplyMeleeDamageToTarget(LocalTargetInfo target)
		{
			ModExt.moonlitAzureBlade?.slashEffect?.Spawn(target.Cell, CasterPawn.Map);
			ModExt.moonlitAzureBlade?.slashSound?.PlayOneShot(new TargetInfo(CasterPawn.Position, CasterPawn.Map));

			return base.ApplyMeleeDamageToTarget(target);
		}
	}
}
