namespace LimbusWeapons
{
	public class AbilityComp_LaevateinnTransform : CompAbilityEffect
	{
		private ThingComp_Laevateinn heatComp;
		private ThingComp_Laevateinn HeatComp => heatComp ??= parent.pawn?.equipment?.Primary?.TryGetComp<ThingComp_Laevateinn>();


		public new CompProperties_LaevateinnTransform Props => (CompProperties_LaevateinnTransform)props;


		public override void Apply(LocalTargetInfo target, LocalTargetInfo dest)
		{
			base.Apply(target, dest);

			Thing newLaev = ThingMaker.MakeThing(Props.nextLaevateinn);
			newLaev.TryGetComp<ThingComp_Laevateinn>()?.Progress = HeatComp.Progress;

			parent.pawn.equipment.Primary.Destroy();
			parent.pawn.equipment.AddEquipment(newLaev as ThingWithComps);
		}


		public override bool GizmoDisabled(out string reason)
		{
			if (base.GizmoDisabled(out reason))
			{
				return true;
			}

			if (HeatComp?.Progress >= Props.progressNeededForUse)
			{
				return false;
			}
			reason = "LaevateinnGizmoDisabled".Translate((NamedArgument)HeatComp?.Progress.ToString("F2"), Props.progressNeededForUse);
			return true;
		}
	}
}
