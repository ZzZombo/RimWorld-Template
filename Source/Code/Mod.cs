using System.Collections.Generic;
using System.Linq;
using HarmonyLib;
using Verse;
using ZzZomboRW.Framework;

namespace ZzZomboRW.Template //*FIXME*
{
	[HotSwappable]
	internal static class MOD
	{
		public const string NAME = "*FIXME*";
		public static readonly string FullID = typeof(MOD).Namespace;
		public static readonly string ID = FullID.Split(new[] { '.' }, 2).Last();
		public static readonly string IDNoDots = ID.Replace('.', '_');
	}

	[HotSwappable]
	internal class Mod: Verse.Mod
	{
		public static Mod Instance;
		public readonly Harmony harmony;
		internal readonly ModSettings settings;
		public static readonly List<Framework.PatchInfo> immediatePatches = new()
		{
		};
		public Mod(ModContentPack content) : base(content)
		{
			Instance ??= this;
			this.harmony = new Harmony(MOD.FullID);
			this.settings = this.GetSettings<ModSettings>();
			FrameworkMod.ApplyPatches(this.harmony, immediatePatches);
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
			return MOD.FullID.TryTranslate(out var r) ? r : MOD.NAME;
		}
#endif
	}
}
