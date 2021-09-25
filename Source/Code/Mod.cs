using System;
using System.Collections.Generic;
using System.Reflection;
using HarmonyLib;
using Verse;
using ZzZomboRW.Framework;

namespace ZzZomboRW.Template //*FIXME*
{
	internal static class MOD
	{
		public const string NAME = "*FIXME*";
		public static readonly string FullID = typeof(Mod).Namespace;
		public static readonly string ID = $"{FullID.Split(".".ToCharArray(), 1)[1]}";
		public static readonly string IDNoDots = ID.Replace('.', '_');
	}
	[HotSwappable]
	internal class Mod: Verse.Mod
	{
		public static Mod Instance;
		public readonly Harmony harmony;
#if MOD_SHOW_SETTINGS
		private readonly ModSettings settings;
#endif
		public Mod(ModContentPack content) : base(content)
		{
			Instance ??= this;
			this.harmony = new Harmony(MOD.FullID);
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
			return MOD.FullID.TryTranslate(out var r) ? r : MOD.NAME;
		}
#endif
	}
}
