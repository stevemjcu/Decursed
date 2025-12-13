using Foster.Framework;
using YetAnotherEcs;
using static Decursed.Components;

namespace Decursed;

internal class Stacking(World world) : System(world)
{
	private static Filter Platforms = new Filter()
		.Include<Position, Platform, Grounded, Hitbox, Active>();

	private static Filter Actors = new Filter()
		.Include<Position, Velocity, Falling, Hitbox, Active>();

	public override void Update(Time time)
	{
		foreach (var id0 in World.View(Actors))
		{
			foreach (var id1 in World.View(Platforms))
			{
				// No collision if actors are the same.
				if (id0 == id1)
				{
					continue;
				}

				var position0 = World.Get<Position>(id0).Value;
				var position1 = World.Get<Position>(id1).Value;

				var rect0 = new Rect(position0, World.Get<Hitbox>(id0).Value);
				var rect1 = new Rect(position1, World.Get<Hitbox>(id1).Value);

				// No collision if actors are non-overlapping or pushout is not upwards.
				if (!rect0.Overlaps(rect1, out var pushout) || pushout.Y >= 0)
				{
					continue;
				}

				var velocity = World.Get<Velocity>(id0).Value;
				World.Set<Velocity>(id0, new(velocity with { Y = 0 }));
				World.Set<Grounded>(id0);
				World.Remove<Falling>(id0);

				World.Set<Position>(id0, new(position0 + pushout));
			}
		}
	}
}
