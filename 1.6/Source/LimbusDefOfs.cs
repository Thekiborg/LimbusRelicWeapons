namespace LimbusWeapons
{
#pragma warning disable CA2211

	[DefOf]
	public static class LimbusDefOfs
	{
		static LimbusDefOfs()
		{
			DefOfHelper.EnsureInitializedInCtor(typeof(LimbusDefOfs));
		}


		public static HediffDef LRW_EyeOfOdin;
		public static BodyPartDef Skull;
		public static HediffDef LRW_EyeSocketBurn;
		public static ThingDef LRW_BladetrailSmall;
		public static ThingDef LRW_BladetrailMedium;
		public static ThingDef LRW_BladetrailBig;
		public static DamageDef LRW_BladetrailCut;
		//public static HediffDef LRW_Muga;
	}
}
