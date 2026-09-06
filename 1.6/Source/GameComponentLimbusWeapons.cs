namespace LimbusWeapons
{
	public class GameComponentLimbusWeapons : GameComponent
	{
		internal static List<ArayashikiCut> ArayashikiCuts = [];
		internal static Dictionary<Pawn, List<ThingClass_Bladetrail>> SpawnedBladetrailsByPawn = [];
		private List<Pawn> _pawns;
		private List<List<ThingClass_Bladetrail>> _bladetrails;
		private const int TicksForArayashikiWoundsReopen = 2000;//GenDate.TicksPerDay;


#pragma warning disable IDE0060
		public GameComponentLimbusWeapons(Game game) { }
#pragma warning restore IDE0060


		public override void GameComponentTick()
		{
			base.GameComponentTick();
			if (Current.Game.tickManager.TicksGame % TicksForArayashikiWoundsReopen == 0)
			{
				for (int i = 0; i < ArayashikiCuts.Count; i++)
				{
					var cutInfo = ArayashikiCuts[i];

					if (cutInfo.pawn is null) continue; // Should I get rid of pawns that are killed?

					Log.Message($"For {cutInfo.pawn} adding hediff at {cutInfo.hediffSeverity} on {cutInfo.bodyPart}");

					if (DropAnyProstheticIfPresent(cutInfo))
						continue;

					if (cutInfo.bodyPart.def.hitPoints <= cutInfo.hediffSeverity)
					{
						Log.Message("Slash destroyed bodypart");
						// Not null means the part is missing
						if (cutInfo.pawn.health.hediffSet.GetMissingPartFor(cutInfo.bodyPart) is HediffWithComps hediff)
						{
							Log.Message("Trying to find parent bionic");
							var foundPart = PartOrAnyAncestorHasDirectlyAddedParts(cutInfo.pawn, cutInfo.bodyPart);
							if (foundPart is not null)
							{
								DropBionicOnPart(cutInfo.pawn, foundPart);
								Log.Message("Found");
							}

							// Only make it bleed if the parent is there. Can't make a toe bleed if the whole leg is gone.
							if (hediff.Part.parent is not null)
								hediff.TryGetComp<HediffComp_TendDuration>()?.tendTicksLeft = -1;
						}
					}
					else
					{
						Log.Message("Slash only injured");

						if (cutInfo.pawn.health.hediffSet.GetMissingPartFor(cutInfo.bodyPart) is null)
						{
							if (!TryGetHediff(cutInfo.pawn, LimbusDefOfs.LRW_ArayashikiSlash, cutInfo.bodyPart, out var hediff))
							{
								Hediff addedHediff = HediffMaker.MakeHediff(LimbusDefOfs.LRW_ArayashikiSlash, cutInfo.pawn, cutInfo.bodyPart);
								addedHediff.Severity = cutInfo.hediffSeverity;
								cutInfo.pawn.health.AddHediff(addedHediff);
								Log.Message(addedHediff.Part.Label);
								continue;
							}
							hediff.Severity = cutInfo.hediffSeverity;
						}
					}
				}
			}
		}


		public static bool TryGetHediff(Pawn pawn, HediffDef def, BodyPartRecord part, out Hediff hediff)
		{
			for (int i = 0; i < pawn.health.hediffSet.hediffs.Count; i++)
			{
				var tempHd = pawn.health.hediffSet.hediffs[i];
				if (tempHd.def == def && tempHd.Part == part)
				{
					hediff = tempHd;
					return true;
				}
			}
			hediff = null;
			return false;
		}


		private static BodyPartRecord PartOrAnyAncestorHasDirectlyAddedParts(Pawn pawn, BodyPartRecord part)
		{
			if (pawn.health.hediffSet.HasDirectlyAddedPartFor(part))
			{
				return part;
			}
			if (part.parent != null && PartOrAnyAncestorHasDirectlyAddedParts(pawn, part.parent) is BodyPartRecord foundParentPart)
			{
				return foundParentPart;
			}
			return null;
		}


		private static bool DropAnyProstheticIfPresent(ArayashikiCut cutInfo)
		{
			for (int j = 0; j < cutInfo.pawn.health.hediffSet.hediffs.Count; j++)
			{
				var hediff = cutInfo.pawn.health.hediffSet.hediffs[j];
				if (hediff.Part != cutInfo.bodyPart)
					continue;

				if (hediff is Hediff_AddedPart)
				{
					DropBionicOnPart(cutInfo.pawn, cutInfo.bodyPart);
					return true;
				}
			}
			return false;
		}


		private static void DropBionicOnPart(Pawn pawn, BodyPartRecord part)
		{
			MedicalRecipesUtility.SpawnThingsFromHediffs(pawn, part, pawn.Position, pawn.Map);
			pawn.TakeDamage(new DamageInfo(DamageDefOf.SurgicalCut, 99999f, 999f, -1f, null, part));
		}


		public override void ExposeData()
		{
			base.ExposeData();
			Scribe_Collections.Look(ref SpawnedBladetrailsByPawn, "LimbusWeapons_SpawnedBladetrailByPawn", LookMode.Reference, LookMode.Reference, ref _pawns, ref _bladetrails);
			Scribe_Collections.Look(ref ArayashikiCuts, "LimbusWeapons_ArayashikiCuts", LookMode.Deep);
		}
	}


	public class ArayashikiCut : IExposable
	{
		public Pawn pawn;
		public BodyPartRecord bodyPart;
		public float hediffSeverity;

		public ArayashikiCut() { }

		public ArayashikiCut(Pawn pawn, BodyPartRecord bodyPart, float hediffSeverity)
		{
			this.pawn = pawn;
			this.bodyPart = bodyPart;
			this.hediffSeverity = hediffSeverity;
		}

		public void ExposeData()
		{
			Scribe_References.Look(ref pawn, "LimbusWeapons_ArayashikiCuts_pawn");
			Scribe_BodyParts.Look(ref bodyPart, "LimbusWeapons_ArayashikiCuts_bodyPart");
			Scribe_Values.Look(ref hediffSeverity, "LimbusWeapons_ArayashikiCuts_hediffSeverity");
		}
	}
}
