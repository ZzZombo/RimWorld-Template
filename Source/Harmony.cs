using HarmonyLib;
using Verse;

namespace ZzZomboRW
{
	[StaticConstructorOnStartup]
	internal static class HarmonyHelper
	{
		static HarmonyHelper()
		{
			var harmony = new Harmony(Mod.ModID);
			harmony.PatchAll();
		}
	}
}
