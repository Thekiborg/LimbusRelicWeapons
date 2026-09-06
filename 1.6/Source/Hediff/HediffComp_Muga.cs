namespace LimbusWeapons
{
	public class HediffComp_Muga : HediffComp
	{
		public int timesUsed;


		public override string CompLabelInBracketsExtra => "ArayashikiLabel".Translate(timesUsed);
		public HediffCompProperties_Muga Props => (HediffCompProperties_Muga)props;



		public void UsedOnce()
		{
			timesUsed++;
			//ForgetRelation();
			if (timesUsed >= Props.usesUntilMuga)
			{
				parent.pawn.mindState.mentalStateHandler.TryStartMentalState(MentalStateDefOf.BerserkPermanent,
					"CausedByHediff".Translate(parent.def.LabelCap), forced: true, forceWake: true, causedByMood: false, null, transitionSilently: true);
			}
		}


		public void ForgetRelation()
		{

			List<Thought_Memory> memories = Pawn.needs.mood.thoughts.memories.Memories;
			memories.Sort((prev, next) => prev.pawn.relations.OpinionOf(prev.otherPawn));

			foreach (var memory in memories)
			{
				Log.Message(memory.otherPawn);
			}
			Messages.Message("Forgot something", MessageTypeDefOf.NegativeEvent);
		}


		public override void CompExposeData()
		{
			base.CompExposeData();
			Scribe_Values.Look(ref timesUsed, "LimbusWeapons_Muga_TimesUsed");
		}
	}
}
