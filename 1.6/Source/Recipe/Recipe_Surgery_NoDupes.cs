namespace LimbusWeapons
{
	public class Recipe_Surgery_NoDupes : Recipe_InstallArtificialBodyPart
	{
		public override bool AvailableOnNow(Thing thing, BodyPartRecord part = null)
		{
			if (thing is not Pawn pawn)
			{
				return false;
			}
			if (pawn.health.hediffSet.HasHediff(recipe.addsHediff))
			{
				return false;
			}
			return base.AvailableOnNow(thing, part);
		}
	}
}
