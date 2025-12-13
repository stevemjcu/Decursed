using Foster.Framework;
using YetAnotherEcs;
using static Decursed.Components;

namespace Decursed;

internal class Collision(World world) : System(world)
{
	private static readonly List<Point2> Adjacencies =
	[
		new(-1, -1), new(+0, -1), new(+1, -1),
		new(-1, +0), new(+0, +0), new(+1, +0),
		new(-1, +1), new(+0, +1), new(+1, +1)
	];

	public override void Update(Time _)
	{
		foreach (var id in World.View(
			new Filter().Include<Position, Velocity, Hitbox>()))
		{
			var position = World.Get<Position>(id).Value;

			foreach (var it in Adjacencies)
			{
				var cell = position.RoundToPoint2() + it;

				// No collision if tile is empty.
				if (Tilemap[cell.X, cell.Y] == 0)
				{
					continue;
				}

				var a = new Rect(position, World.Get<Hitbox>(id).Value);
				var b = new Rect(cell, Config.UnitSize);

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

				if (pushout.Y < 0)
				{
					var velocity = World.Get<Velocity>(id).Value;
					World.Set(id, new Velocity(velocity with { Y = 0 }));
					World.Set(id, new Grounded());
				}

				World.Set(id, new Position(position + pushout));
			}
		}
	}
}
