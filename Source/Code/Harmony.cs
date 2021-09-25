using System.Reflection;
using HarmonyLib;
using Verse;
using ZzZomboRW.Framework;

namespace ZzZomboRW.Template //*FIXME*
{
	[StaticConstructorOnStartup]
	[HotSwappable]
	internal static class StartupHarmonyHelper
	{
		static StartupHarmonyHelper()
		{
			Mod.Instance.harmony.PatchAll(Assembly.GetExecutingAssembly());
		}
	}
}
