namespace LimbusWeapons
{
	public class Verb_Arayashiki : Verb_MeleeAttackDamage
	{
		protected override DamageWorker.DamageResult ApplyMeleeDamageToTarget(LocalTargetInfo target)
		{
			//var hediff = CasterPawn.health.GetOrAddHediff(LimbusDefOfs.LRW_Muga);
			//hediff.TryGetComp<HediffComp_Muga>()?.UsedOnce();
			return base.ApplyMeleeDamageToTarget(target);
		}
	}
}
