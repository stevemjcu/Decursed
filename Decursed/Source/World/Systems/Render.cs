using Foster.Framework;
using System.Numerics;
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
				if (Tilemap[x, y] > 0)
				{
					Graphics.Batcher.Image(
						Graphics.Atlas.Get(Config.Spritesheet.Tiles, 0),
						Graphics.Camera.WorldToNative(new(x, y)),
						Color.White);
				}
			}
		}

		foreach (var it in World.View(new Filter().Include<Sprite, Position, Focused>()))
		{
			var sprite = it.Get<Sprite>().Value;
			var position = it.Get<Position>().Value;
			var orientation = it.TryGet<Orientation>(out var c) ? c.Value : new(-1, 1);

			Graphics.Batcher.Image(
				Graphics.Atlas.Get(Config.Spritesheet.Actors, sprite),
				Graphics.Camera.WorldToNative(position),
				Graphics.Camera.WorldToNative((Vector2.One - orientation).Normalized()),
				Vector2.One * orientation,
				0, Color.White);
		}
	}
}
