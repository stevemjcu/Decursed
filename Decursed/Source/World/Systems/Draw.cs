using Foster.Framework;
using YetAnotherEcs;
using static Decursed.Components;

namespace Decursed;

internal class Draw(World World, Graphics Graphics) : System(World)
{
	public override void Tick(Time time)
	{
		// Draw environment

		var template = World.Get<InstanceOf>(Instance).Id;
		var tilemap = World.Get<Tilemap>(template).Value;

		for (var x = 0; x < tilemap.GetLength(0); x++)
		{
			for (var y = 0; y < tilemap.GetLength(1); y++)
			{
				if (tilemap[x, y][0] != 'w')
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

		// Draw actors

		var view0 = World.View(new Filter().Include<Sprite>().Include<Position>());
		var view1 = World.View(new ChildOf(Instance));

		foreach (var it in view0.Intersect(view1))
		{
			var sprite = World.Get<Sprite>(it).Index;
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
