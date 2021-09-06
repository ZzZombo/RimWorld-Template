using UnityEngine;
using Verse;
using RimWorld;
using System;

internal static class MOD
{
	public const string ID = "*FIXME*";
	public const string NAME = "*FIXME*";
	public const bool SHOW_SETTINGS = false;
}

namespace ZzZomboRW
{
	[HotSwappable]
	internal class Mod: Verse.Mod
	{
		private ModSettings settings;
		public static string ModID => $"{typeof(Mod).Namespace}.{MOD.ID}";
		public Mod(ModContentPack content) : base(content)
		{
			this.settings = this.GetSettings<ModSettings>();
		}
		/* public override void DoSettingsWindowContents(Rect rect)
		{
			var listingStandard = new Listing_Standard();
			listingStandard.Begin(rect);
			listingStandard.End();
			base.DoSettingsWindowContents(rect);
		} */
		public override string SettingsCategory()
		{
			if(!MOD.SHOW_SETTINGS)
			{
				return null;
			}
#pragma warning disable CS0162 // Unreachable code detected.
			if(ModID.TryTranslate(out var r))
			{
				return r;
			}
			return MOD.NAME;
#pragma warning restore CS0162 // Unreachable code detected.
		}
	}
}
