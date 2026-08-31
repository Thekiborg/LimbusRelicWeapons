namespace LimbusWeapons
{
	public class ModExtension : DefModExtension
	{
		public BladetrailSettings bladetrail;
		public MoonlitAzureBladeSettings moonlitAzureBlade;
		public ArayashikiSettings arayashiki;
	}


	public class ArayashikiSettings
	{
		public ThingDef arayashikiSheathed;
		public ThingDef arayashikiUnsheathed;
	}


	public class MoonlitAzureBladeSettings
	{
		public EffecterDef slashEffect;
		public SoundDef slashSound;
	}


	public class BladetrailSettings
	{
		public int hoursExist;
		public EffecterDef effecterOnDestroy;
	}
}
