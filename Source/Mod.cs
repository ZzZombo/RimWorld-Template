using Verse;

internal static class MOD
{
	public const string ID = "*FIXME*";
	public const string NAME = "*FIXME*";
}

namespace ZzZomboRW
{
	[HotSwappable]
	internal class Mod: Verse.Mod
	{
		public static readonly string NAMESPACE = typeof(Mod).Namespace;
		public static readonly string ModID = $"{NAMESPACE}.{MOD.ID}";
		public static readonly string ModIDNoDots = ModID.Replace('.', '_');
		public static Mod Instance;
#if MOD_SHOW_SETTINGS
		private readonly ModSettings settings;
#endif
		public Mod(ModContentPack content) : base(content)
		{
			Instance ??= this;
#if MOD_SHOW_SETTINGS
			this.settings = this.GetSettings<ModSettings>();
#endif
		}
#if MOD_SHOW_SETTINGS
		public override void DoSettingsWindowContents(UnityEngine.Rect rect)
		{
			var listingStandard = new Listing_Standard();
			listingStandard.Begin(rect);
			// TODO: add code.
			listingStandard.End();
			base.DoSettingsWindowContents(rect);
		}
		public override string SettingsCategory()
		{
			return ModID.TryTranslate(out var r) ? (string)r : MOD.NAME;
		}
#endif
	}
}
