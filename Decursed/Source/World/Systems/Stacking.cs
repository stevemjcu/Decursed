using Foster.Framework;
using YetAnotherEcs;
using static Decursed.Components;

namespace Decursed;

internal class Stacking(World world) : System(world)
{
	public override void Update(Time time)
	{
		var repeat = true;
		for (var i = 0; repeat && i < 10; i++)
		{
			repeat = false;
			foreach (var a in World.View(
				new Filter().Include<Position, Hitbox, Platform, Grounded, Active>()))
			{
				foreach (var b in World.View(
					new Filter().Include<Position, Hitbox, Velocity, Falling, Active>()))
				{
					// No collision if actor is this platform
					if (a == b)
					{
						continue;
					}

					var position0 = a.Get<Position>().Value;
					var position1 = b.Get<Position>().Value;

					var rect0 = a.Get<Hitbox>().Value.Translate(position0);
					var rect1 = b.Get<Hitbox>().Value.Translate(position1);

					// No collision if actor is not overlapping or pushout is not upwards.
					if (!rect1.Overlaps(rect0, out var pushout) || pushout.Y >= 0)
					{
						continue;
					}

					var prevPosition = position1 - b.Get<Velocity>().Value * time.Delta;
					var prevRect = b.Get<Hitbox>().Value.Translate(prevPosition);

					// No collision if actor was already colliding with platform.
					if (prevRect.Overlaps(rect0, out var margin) && margin.Length() > 0.05)
					{
						continue;
					}

					b.Set<Position>(new(position1 + pushout));

					var velocity = b.Get<Velocity>().Value;
					b.Set<Velocity>(new(velocity with { Y = 0 }));
					b.Remove<Falling>();
					b.Set<Grounded>();

					// Since collision occurred, another may after pushout.
					repeat = true;
				}
			}
		}
	}
}
