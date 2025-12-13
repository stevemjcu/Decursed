using Foster.Framework;
using YetAnotherEcs;
using static Decursed.Components;

namespace Decursed;

internal class Render(World World, Graphics Graphics) : System(World)
{
	public override void Update(Time time)
	{
		for (var x = 0; x < Tilemap.GetLength(0); x++)
		{
			for (var y = 0; y < Tilemap.GetLength(1); y++)
			{
				if (Tilemap[x, y] == 0)
				{
					continue;
				}

				Graphics.Batcher.Image
				(
					Graphics.Atlas.Get(Config.Spritesheet.Tiles, 0),
					Graphics.Camera.WorldToNative(new(x, y)),
					Color.White
				);
			}
		}

		foreach (var it in World
			.View(new Filter().Include<Sprite, Position, Active>()))
		{
			var sprite = World.Get<Sprite>(it).Value;
			var position = World.Get<Position>(it).Value;

			Graphics.Batcher.Image
			(
				Graphics.Atlas.Get(Config.Spritesheet.Actors, sprite),
				Graphics.Camera.WorldToNative(position),
				Color.White
			);
		}
	}
}
