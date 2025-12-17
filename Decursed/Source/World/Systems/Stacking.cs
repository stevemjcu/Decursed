using Foster.Framework;
using YetAnotherEcs;
using static Decursed.Components;

namespace Decursed;

internal class Stacking(World world) : System(world)
{
	private static Filter Bodies = new Filter()
		.Include<Position, Velocity, Falling, Hitbox, Active>();

	private static Filter Platforms = new Filter()
		.Include<Position, Platform, Grounded, Hitbox, Active>();

	public override void Update(Time _)
	{
		var repeat = true;
		for (var i = 0; repeat && i < 10; i++)
		{
			repeat = false;
			foreach (var b in World.View(Bodies))
			{
				foreach (var a in World.View(Platforms))
				{
					// No collision if actor is this platform
					if (b == a)
					{
						continue;
					}

					var position0 = b.Get<Position>().Value;
					var position1 = a.Get<Position>().Value;

					var rect0 = b.Get<Hitbox>().Value.Translate(position0);
					var rect1 = a.Get<Hitbox>().Value.Translate(position1);

					// No collision if actor is not overlapping or pushout is not upwards.
					if (!rect0.Overlaps(rect1, out var pushout) || pushout.Y >= 0)
					{
						continue;
					}

					b.Set<Position>(new(position0 + pushout));

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
