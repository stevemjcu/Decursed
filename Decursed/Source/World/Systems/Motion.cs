using Foster.Framework;
using YetAnotherEcs;
using static Decursed.Components;

namespace Decursed;

internal class Motion(World world) : System(world)
{
	private static readonly List<Point2> Adjacencies =
	[
		new(-1, -1), new(+0, -1), new(+1, -1),
		new(-1, +0), new(+0, +0), new(+1, +0),
		new(-1, +1), new(+0, +1), new(+1, +1)
	];

	public override void Tick(Time time)
	{
		var view = World.View(new Filter().Include<Position, Velocity>());
		var tilemap = World.Get<Tilemap>(Instance).Value;

		foreach (var id in view)
		{
			var position0 = World.Get<Position>(id).Value;
			var velocity = World.Get<Velocity>(id).Value;
			var gravity = World.Has<Gravity>(id);

			if (gravity)
			{
				velocity.Y += Config.Gravity * time.Delta;
				World.Remove<Grounded>(id);
			}

			velocity = velocity.Clamp(
				new(-Config.TerminalSpeed, -Config.TerminalSpeed),
				new(+Config.TerminalSpeed, +Config.TerminalSpeed));

			var displacement = velocity * time.Delta;
			var position1 = position0 + displacement;

			if (World.TryGet<Hitbox>(id, out var bounds))
			{
				foreach (var it in Adjacencies)
				{
					var cellA = position1.RoundToPoint2();
					var rectA = new Rect(position1, bounds.Value);

					var cellB = cellA + it;
					var rectB = new Rect(cellB, Config.UnitSize);

					if (tilemap[cellB.X, cellB.Y] > 0 && rectA.Overlaps(rectB, out var pushout))
					{
						var cellC = cellB + pushout.Normalized().RoundToPoint2();

						if (tilemap[cellC.X, cellC.Y] == 0)
						{
							position1 += pushout;

							if (pushout.Y < 0)
							{
								velocity.Y = 0;
								World.Set(id, new Grounded());
							}
						}
					}
				}
			}

			World.Set(id, new Position(position1));
			World.Set(id, new Velocity(velocity));
		}
	}
}
