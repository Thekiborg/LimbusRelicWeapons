global using HarmonyLib;
global using RimWorld;
global using System;
global using System.Collections.Generic;
global using UnityEngine;
global using Verse;

namespace LimbusWeapons
{
	[StaticConstructorOnStartup]
	public static class LimbusWeapons
	{
		static LimbusWeapons()
		{
			Harmony harmony = new("Thekiborg.LimbusWeapons");
			harmony.PatchAll();
		}
	}
}
