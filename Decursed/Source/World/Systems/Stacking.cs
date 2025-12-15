using Foster.Framework;
using YetAnotherEcs;
using static Decursed.Components;

namespace Decursed;

internal class Stacking(World world) : System(world)
{
	private static Filter Actors = new Filter()
		.Include<Position, Velocity, Falling, Hitbox, Active>();

	private static Filter Platforms = new Filter()
		.Include<Position, Platform, Grounded, Hitbox, Active>();

	public override void Update(Time time)
	{
		foreach (var a in World.View(Actors))
		{
			foreach (var b in World.View(Platforms))
			{
				// No collision if actors are the same.
				if (a == b)
				{
					continue;
				}

				var position0 = a.Get<Position>().Value;
				var position1 = b.Get<Position>().Value;

				var rect0 = a.Get<Hitbox>().Value.Translate(position0);
				var rect1 = b.Get<Hitbox>().Value.Translate(position1);

				// No collision if actors are non-overlapping or pushout is not upwards.
				if (!rect0.Overlaps(rect1, out var pushout) || pushout.Y >= 0)
				{
					continue;
				}

				var velocity = a.Get<Velocity>().Value;
				a.Set<Velocity>(new(velocity with { Y = 0 }));
				a.Set<Position>(new(position0 + pushout));
				a.Remove<Falling>().Set<Grounded>();
			}
		}

		var view = World.View<HeldBy>(new(Player));
		if (view.Count > 0 && view[0] is var item)
		{
			var position = Player.Get<Position>().Value;
			item.Set<Position>(new(position - Config.HoldOffset));
		}
	}
}
