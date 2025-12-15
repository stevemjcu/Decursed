using Foster.Framework;
using YetAnotherEcs;
using static Decursed.Components;

namespace Decursed;

internal class Holding(World world) : System(world)
{
	private static Filter Items = new Filter().Include<Position, HeldBy, Active>();

	public override void Update(Time time)
	{
		foreach (var it in World.View(Items))
		{
			var parent = it.Get<HeldBy>().Value;
			var position = parent.Get<Position>().Value;

			it.Set<Position>(new(position + Config.HoldOffset));
		}
	}
}
