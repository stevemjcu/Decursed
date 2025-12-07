using Foster.Framework;
using YetAnotherEcs;
using static Decursed.Components;

namespace Decursed;

internal class Draw(World World, Graphics Graphics) : System(World)
{
	public override void Update()
	{
		var player = World.View(new Filter().Include<Receiver>())[0];
		var instance = World.Get<ChildOf>(player).Id;
		var template = World.Get<InstanceOf>(instance).Id;
		var tilemap = World.Get<Tilemap>(template).Value;

		for (var x = 0; x < tilemap.GetLength(0); x++)
		{
			for (var y = 0; y < tilemap.GetLength(0); y++)
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

		var view0 = World.View(new Filter().Include<Sprite>().Include<Position>());
		var view1 = World.View(new ChildOf(instance));

		foreach (var it in view1.Intersect(view0))
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
