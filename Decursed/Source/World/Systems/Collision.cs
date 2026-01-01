using Foster.Framework;
using YetAnotherEcs;
using static Decursed.Components;

namespace Decursed;

internal class Collision(World world) : System(world)
{
	public override void Update(Time _)
	{
		foreach (var it in World.View(new Filter().Include<Position, Hitbox, Focused>()))
		{
			foreach (var dir in Config.Directions)
			{
				var position = it.Get<Position>().Value;
				var cell = position.RoundToPoint2() + dir;

				// No collision if tile is empty.
				if (Tilemap[cell.X, cell.Y] == 0)
				{
					continue;
				}

				var a = it.Get<Hitbox>().Value.Translate(position);
				var b = Config.StandardBox.Translate(cell);

				// No collision if tile is not overlapping.
				if (!a.Overlaps(b, out var pushout))
				{
					continue;
				}

				cell += pushout.Normalized().RoundToPoint2();

				// No collision if projected tile is not empty.
				if (Tilemap[cell.X, cell.Y] != 0)
				{
					continue;
				}

				if (it.Has<Velocity>())
				{
					var velocity = it.Get<Velocity>().Value;

					if (pushout.Y > 0 && velocity.Y < 0)
					{
						it.Set<Velocity>(new(velocity.ZeroY()));
					}

					if (pushout.Y < 0 && velocity.Y > 0)
					{
						it.Set<Velocity>(new(velocity.ZeroY()));
						it.Set<Grounded>();
					}

					if (pushout.X != 0 && !it.Has<Gravity>())
					{
						it.Set<Velocity>(new(velocity * Config.ThrowRecoil));
						it.Set<Gravity>();
					}
				}

				it.Set<Position>(new(position + pushout));
			}
		}
	}
}
