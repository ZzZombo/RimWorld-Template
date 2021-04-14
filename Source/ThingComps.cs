using Verse;

namespace ZzZomboRW
{
	/* public class CompProperties_X: CompProperties
	{
		public bool enabled = true;
		public int data = -1;
		public CompProperties_X()
		{
			this.compClass = typeof(CompX);
			Log.Message($"[{this.GetType().FullName}] Initialized:\n" +
				$"\tData: {this.data};\n" +
				$"\tEnabled: {this.enabled}.");
		}

	} */
	/* public class CompX: ThingComp
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
	} */
}
