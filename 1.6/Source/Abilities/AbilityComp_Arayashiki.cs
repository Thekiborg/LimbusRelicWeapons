
using Verse.Sound;

namespace LimbusWeapons
{
	public class AbilityComp_Arayashiki : CompAbilityEffect
	{
		public new CompProperties_Arayashiki Props => (CompProperties_Arayashiki)props;


		public override void Apply(LocalTargetInfo target, LocalTargetInfo dest)
		{
			base.Apply(target, dest);
			parent.pawn.equipment.Primary.Destroy();
			Thing newWeapon = ThingMaker.MakeThing(Props.switchTo);
			parent.pawn.equipment.AddEquipment(newWeapon as ThingWithComps);

			Props.sheathingSound?.PlayOneShot(new TargetInfo(parent.pawn.Position, parent.pawn.Map));
		}
	}
}
