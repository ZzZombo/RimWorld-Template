using UnityEngine;
using HarmonyLib;
using Verse;
using RimWorld;

internal static class MOD
{
    public const string NAME = "MOD";
}

namespace ZzZomboRW
{
    public class CompProperties_X : CompProperties
    {
        public bool enabled = true;
        public int data = -1;
        public CompProperties_X()
        {
            this.compClass = typeof(CompX);
            Log.Message($"[{this.GetType().FullName}] Initialized:\n" +
                $"\tCurrent ammo: {this.data};\n" +
                $"\tEnabled: {this.enabled}.");
        }

    }
    public class CompX : ThingComp
    {
        public CompProperties_X Props => (CompProperties_X)this.props;
        public bool Enabled => this.Props.enabled;
        public override void Initialize(CompProperties props)
        {
            base.Initialize(props);
            Log.Message($"[{this.GetType().FullName}] Initialized for {this.parent}:\n" +
                $"\tData: {this.Props.data};\n" +
                $"\tEnabled: {this.Props.enabled}.");
        }

        public override void PostExposeData()
        {
            Scribe_Values.Look(ref this.Props.data, "data", 1, false);
            Scribe_Values.Look(ref this.Props.enabled, "enabled", true, false);
        }

        [StaticConstructorOnStartup]
        internal static class HarmonyHelper
        {
            static HarmonyHelper()
            {
                Harmony harmony = new Harmony($"ZzZomboRW.{MOD.NAME}");
                harmony.PatchAll();
            }

            [HarmonyPatch(typeof(object), nameof(object.Equals))]
            public static class Class_MethodPatch
            {
                private static void Postfix(ref object __result, object __instance)
                {
                }
            }
        }
    }
}
