using Foster.Framework;
using YetAnotherEcs;
using static Decursed.Components;

namespace Decursed;

internal class Collision(World world) : System(world)
{
	public override void Update(Time _)
	{
		foreach (var id in World
			.View(new Filter().Include<Position, Hitbox, Active>()))
		{
			foreach (var it in Config.Directions)
			{
				var position = World.Get<Position>(id).Value;
				var cell = position.RoundToPoint2() + it;

				// No collision if tile is empty.
				if (Tilemap[cell.X, cell.Y] == 0)
				{
					continue;
				}

				var a = World.Get<Hitbox>(id).Value.Translate(position);
				var b = Config.StandardBox.Translate(cell);

				// No collision if tile is non-overlapping.
				if (!a.Overlaps(b, out var pushout))
				{
					continue;
				}

				cell += pushout.Normalized().RoundToPoint2();

				// No collision if projected tile is non-empty.
				if (Tilemap[cell.X, cell.Y] != 0)
				{
					continue;
				}

				if (pushout.Y < 0 && World.Has<Velocity>(id))
				{
					var velocity = World.Get<Velocity>(id).Value;
					World.Set<Velocity>(id, new(velocity with { Y = 0 }));
					World.Set<Grounded>(id);
					World.Remove<Falling>(id);
				}

				World.Set<Position>(id, new(position + pushout));
			}
		}
	}
}
