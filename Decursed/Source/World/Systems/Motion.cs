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
		var tilemap = World.Get<Tilemap>(Instance).Map;

		foreach (var id in view)
		{
			var position0 = World.Get<Position>(id).Vector;
			var velocity = World.Get<Velocity>(id).Vector;

			var displacement = velocity * time.Delta;
			var position1 = position0 + displacement;

			if (World.TryGet<Bounds>(id, out var bounds))
			{
				foreach (var it in Adjacencies)
				{
					var cellA = position1.RoundToPoint2();
					var rectA = new Rect(position1, bounds.Vector);

					var cellB = cellA + it;
					var rectB = new Rect(cellB, Config.UnitSize);

					// TODO: Ignore pushout if it'd push into another tile
					if (tilemap[cellB.X, cellB.Y] > 0 && rectA.Overlaps(rectB, out var pushout))
					{
						position1 += pushout;
					}
				}
			}

			World.Set(id, new Position(position1));
		}

		Console.WriteLine(World.Get<Position>(Player).Vector.Y);
	}
}
