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
		public static readonly string NAME = Utils.GetCallingModName(0);
		public static readonly string ID = typeof(MOD).Namespace;
		public static readonly string IDNoDots = ID.Replace('.', '_');
		public static readonly string IDNoRoot = ID.Split(new[] { '.' }, 2).Last();
		public static readonly string IDNoRootNoDots = IDNoRoot.Replace('.', '_');
	}

	[HotSwappable]
	internal class Mod: Verse.Mod
	{
		public static Mod Instance;
		public static readonly Harmony Harmony = new(MOD.ID);
		internal readonly ModSettings settings;
		public static readonly List<Framework.PatchInfo> immediatePatches = new()
		{
		};
		public Mod(ModContentPack content) : base(content)
		{
			Instance ??= this;
			this.settings = this.GetSettings<ModSettings>();
			FrameworkMod.ApplyPatches(immediatePatches);
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
			return MOD.ID.TryTranslate(out var r) ? r : MOD.NAME;
		}
#endif
	}
}
