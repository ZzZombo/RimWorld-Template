using HarmonyLib;
using Verse;

namespace ZzZomboRW
{
	/* [StaticConstructorOnStartup]
	internal static class HarmonyHelper
	{
		static HarmonyHelper()
		{
			var harmony = new Harmony($"ZzZomboRW.{MOD.NAME}");
			harmony.PatchAll();
		}

		[HarmonyPatch(typeof(object), nameof(object.Equals))]
		public static class Class_MethodPatch
		{
			private static void Postfix(ref object __result, object __instance)
			{
			}
		}
	} */
}
