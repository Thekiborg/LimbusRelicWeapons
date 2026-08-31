namespace LimbusWeapons
{
	public class GameComponentLimbusWeapons : GameComponent
	{
		internal static List<ArayashikiCuts> ArayashikiCuts = [];
		internal static Dictionary<Pawn, List<ThingClass_Bladetrail>> SpawnedBladetrailsByPawn = [];
		private List<Pawn> _pawns;
		private List<List<ThingClass_Bladetrail>> _bladetrails;


#pragma warning disable IDE0060
		public GameComponentLimbusWeapons(Game game) { }
#pragma warning restore IDE0060


		public override void GameComponentTick()
		{
			base.GameComponentTick();
			/*if (Current.Game.tickManager.TicksGame % 1000 == 0)
			{
				for (int i = 0; i < ArayashikiCuts.Count; i++)
				{
					var cutInfo = ArayashikiCuts[i];
					if (!cutInfo.pawn.health.hediffSet.HasDirectlyAddedPartFor(cutInfo.bodyPart))
					{
						if (cutInfo.hediffSeverity >= cutInfo.bodyPart.def.hitPoints)
						{

						}
					}
				}
			}*/
		}


		public override void ExposeData()
		{
			base.ExposeData();
			Scribe_Collections.Look(ref SpawnedBladetrailsByPawn, "LimbusWeapons_SpawnedBladetrailByPawn", LookMode.Reference, LookMode.Reference, ref _pawns, ref _bladetrails);
			Scribe_Collections.Look(ref ArayashikiCuts, "LimbusWeapons_ArayashikiCuts", LookMode.Deep);
		}
	}


	public class ArayashikiCuts : IExposable
	{
		public Pawn pawn;
		public BodyPartRecord bodyPart;
		public float hediffSeverity;

		public void ExposeData()
		{
			Scribe_References.Look(ref pawn, "LimbusWeapons_ArayashikiCuts_pawn");
			Scribe_BodyParts.Look(ref bodyPart, "LimbusWeapons_ArayashikiCuts_bodyPart");
			Scribe_Values.Look(ref hediffSeverity, "LimbusWeapons_ArayashikiCuts_hediffSeverity");
		}
	}
}
