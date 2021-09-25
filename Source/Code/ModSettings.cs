using System.Collections.Generic;
using UnityEngine;
using Verse;
using ZzZomboRW.Framework;

namespace ZzZomboRW.Template //*FIXME*
{
	[HotSwappable]
	internal class ModSettings: Verse.ModSettings
	{
		//public object value;
		//public List<object> list = new List<object>();

		public override void ExposeData()
		{
			base.ExposeData();
			//Scribe_Values.Look(ref value, nameof(value));
			//Scribe_Collections.Look(ref list, nameof(list), LookMode.Reference);
		}
	}
}
