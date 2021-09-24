using System.Collections.Generic;
using System.Reflection;
using Verse;
using ZzZomboRW.Framework;

namespace ZzZomboRW.Template //*FIXME*
{
	[StaticConstructorOnStartup]
	[HotSwappable]
	internal static class StartupHarmonyHelper
	{
		public static readonly List<PatchInfo> delayedPatches = new()
		{
		};
		static StartupHarmonyHelper()
		{
			FrameworkMod.ApplyPatches(Mod.Instance.harmony, delayedPatches);
			Mod.Instance.harmony.PatchAll(Assembly.GetExecutingAssembly());
		}
	}
}
