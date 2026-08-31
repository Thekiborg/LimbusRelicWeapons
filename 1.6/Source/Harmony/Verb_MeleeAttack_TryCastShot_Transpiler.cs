using System.Reflection.Emit;

namespace LimbusWeapons
{
	[HarmonyPatch(typeof(Verb_MeleeAttack), "TryCastShot")]
	internal static class Verb_MeleeAttack_TryCastShot_Transpiler
	{
		private static void UseUpEyeOfOdin(Pawn pawn)
		{
			if (pawn is null || pawn.health is null || pawn.health.hediffSet is null)
				return;

			if (!pawn.health.hediffSet.TryGetHediff(LimbusDefOfs.LRW_EyeOfOdin, out var hediff))
				return;

			if (!hediff.TryGetComp<HediffComp_EyeOfOdin>(out var comp))
				return;

			comp.Dodged();
		}


		[HarmonyTranspiler]
		internal static IEnumerable<CodeInstruction> UseUpEyeOfOdinWhenDodging(IEnumerable<CodeInstruction> codeInstructions)
		{
			CodeMatcher codeMatcher = new(codeInstructions);

			var instructionsToMatch = new CodeMatch[]
			{
				new(OpCodes.Ldloc_S),
				new(OpCodes.Ldloc_S),
				new(OpCodes.Ldstr, "TextMote_Dodge"),
				new(OpCodes.Call),
				new(OpCodes.Call),
				new(OpCodes.Ldc_R4),
				new(OpCodes.Call),
			};

			codeMatcher.End();
			codeMatcher.MatchEndBackwards();
			codeMatcher.Advance();

			var instructionsToInsert = new CodeInstruction[]
			{
				new(OpCodes.Ldloc_3),
				new(OpCodes.Call, AccessTools.Method(typeof(Verb_MeleeAttack_TryCastShot_Transpiler), nameof(UseUpEyeOfOdin))),
			};

			codeMatcher.Insert(instructionsToInsert);

			return codeMatcher.InstructionEnumeration();
		}
	}
}
