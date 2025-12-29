using Foster.Framework;
using YetAnotherEcs;
using static Decursed.Components;

namespace Decursed;

internal class Holding(World world) : System(world) {
	public override void Update(Time _) {
		foreach (var it in World.View(new Filter().Include<Position, HeldBy, Focused>())) {
			var parent = it.Get<HeldBy>().Value;
			var position = parent.Get<Position>().Value;

			it.Set<Position>(new(position + Config.HoldOffset));
		}
	}
}
