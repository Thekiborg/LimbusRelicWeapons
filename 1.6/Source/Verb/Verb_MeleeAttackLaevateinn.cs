
namespace LimbusWeapons
{
	public class Verb_MeleeAttackLaevateinn : Verb_MeleeAttackDamage
	{
		protected override DamageWorker.DamageResult ApplyMeleeDamageToTarget(LocalTargetInfo target)
		{
			CasterPawn?.equipment?.Primary?.TryGetComp<ThingComp_Laevateinn>()?.Attacked();
			return base.ApplyMeleeDamageToTarget(target);
		}
	}
}
