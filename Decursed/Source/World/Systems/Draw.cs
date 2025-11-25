using Foster.Framework;
using MoonTools.ECS;
using static Decursed.Components;

namespace Decursed;

/// <summary>
/// Renders the current room and its contents.
/// </summary>
internal class Draw(World world, Factory factory) : System(world)
{
	private readonly Factory Factory = factory;

	public override void Update()
	{
		var player = World.GetSingletonEntity<Receiver>();
		var instance = World.OutRelationSingleton<ChildOf>(player);
		var template = World.OutRelationSingleton<InstanceOf>(instance);

		// TODO: Store tilemap on instance
		var layout = Factory.Layouts[template];

		for (var x = 0; x < layout.GetLength(0); x++)
		{
			for (var y = 0; y < layout.GetLength(0); y++)
			{
				if (layout[x, y][0] != 'w') continue;

				Game.Batcher.Image
				(
					Game.Atlas.Get(Config.Spritesheet.Tiles, 0),
					Game.Camera.WorldToNative(new(x, y)),
					Color.White
				);
			}
		}

		foreach (var it in World.InRelations<ChildOf>(instance))
		{
			if (!World.TryGet<Sprite>(it, out var sprite) ||
				!World.TryGet<Position>(it, out var position)) continue;

			Game.Batcher.Image
			(
				Game.Atlas.Get(Config.Spritesheet.Actors, sprite.Index),
				Game.Camera.WorldToNative(position.Vector),
				Color.White
			);
		}
	}
}
