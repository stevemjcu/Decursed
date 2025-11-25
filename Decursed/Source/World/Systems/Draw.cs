using Foster.Framework;
using MoonTools.ECS;
using static Decursed.Components;

namespace Decursed;

internal class Draw(World world, Factory factory) : System(world)
{
	private readonly Factory Factory = factory;

	public override void Update()
	{
		var player = World.GetSingletonEntity<Receiver>();
		var instance = World.OutRelationSingleton<ChildOf>(player);
		var template = World.OutRelationSingleton<InstanceOf>(instance);
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

		foreach (var entity in World.InRelations<ChildOf>(instance))
		{
			if (!World.Has<Sprite>(entity)) continue;

			var position = World.Get<Position>(entity);
			var sprite = World.Get<Sprite>(entity);

			Game.Batcher.Image
			(
				Game.Atlas.Get(Config.Spritesheet.Actors, sprite.Index),
				Game.Camera.WorldToNative(position.Vector),
				Color.White
			);
		}
	}
}
