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
			ForgetRelation();
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
	}
}
