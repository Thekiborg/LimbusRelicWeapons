using Verse.Sound;

namespace LimbusWeapons
{
	public class AbilityComp_DihuiSheath : CompAbilityEffect
	{
		public new CompProperties_DihuiSheath Props => (CompProperties_DihuiSheath)props;


		public override void Apply(LocalTargetInfo target, LocalTargetInfo dest)
		{
			base.Apply(target, dest);

			if (!Props.unsheating)
			{
				if (DetonateBladetrail())
				{
					Props.soundRend?.PlayOneShot(new TargetInfo(parent.pawn.Position, parent.pawn.Map));
					Props.effecterRend?.Spawn(parent.pawn, parent.pawn.Map);
				}
			}

			parent.pawn.equipment.Primary.Destroy();
			Thing newWeapon = ThingMaker.MakeThing(Props.switchTo);
			parent.pawn.equipment.AddEquipment(newWeapon as ThingWithComps);

			Props.sheathingSound?.PlayOneShot(new TargetInfo(parent.pawn.Position, parent.pawn.Map));
		}


		private bool DetonateBladetrail()
		{
			ThingDef sword = parent.pawn.equipment?.Primary.def;
			bool keyFound = GameComponentLimbusWeapons.SpawnedBladetrailsByPawn.TryGetValue(parent.pawn, out var bladetrails);
			if (!keyFound || bladetrails.NullOrEmpty())
				return false;


			for (int i = bladetrails.Count - 1; i >= 0; i--)
			{
				var bladetrail = bladetrails[i];

				var cells = GenAdj.CellsOccupiedBy(bladetrail);
				foreach (var cell in cells)
				{
					var buildingsOnCell = bladetrail.Map.thingGrid.ThingsListAt(cell);
					for (int j = buildingsOnCell.Count - 1; j >= 0; j--)
					{
						var thing = buildingsOnCell[j];
						if (thing is null) continue;
						if (thing is Pawn pawn && pawn == parent.pawn) continue;

						DamageInfo dinfo = new(LimbusDamageDefOfs.LRW_BladetrailCut, Props.bladetrailDamage, Props.bladetrailPen, weapon: sword);
						thing.TakeDamage(dinfo);
					}
				}

				bladetrail.Destroy();
			}

			return true;
		}
	}
}
