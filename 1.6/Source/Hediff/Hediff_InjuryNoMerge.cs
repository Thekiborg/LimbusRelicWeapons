namespace LimbusWeapons
{
	public class Hediff_InjuryNoMerge : Hediff_Injury
	{
		public override bool TryMergeWith(Hediff other)
		{
			return false;
		}
	}
}
