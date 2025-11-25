using Foster.Framework;
using MoonTools.ECS;
using static Decursed.Components;

namespace Decursed;

internal class Draw
(
	World world,
	Batcher batcher,
	Camera camera,
	Atlas atlas,
	Dictionary<Entity, string[,]> layouts
)
	: Renderer(world)
{
	private readonly Batcher Batcher = batcher;
	private readonly Camera Camera = camera;
	private readonly Atlas Atlas = atlas;

	public Dictionary<Entity, string[,]> Layouts = layouts;

	public void Render()
	{
		var player = World.GetSingletonEntity<Receiver>();
		var instance = World.OutRelationSingleton<ChildOf>(player);
		var template = World.OutRelationSingleton<InstanceOf>(instance);
		var layout = Layouts[template];

		for (var x = 0; x < layout.GetLength(0); x++)
		{
			for (var y = 0; y < layout.GetLength(0); y++)
			{
				if (layout[x, y][0] != 'w') continue;

				Batcher.Image
				(
					Atlas.Get(Spritesheet.Tiles, 0),
					Camera.WorldToNative(new(x, y)),
					Color.White
				);
			}
		}

		foreach (var entity in World.InRelations<ChildOf>(instance))
		{
			if (!World.Has<Sprite>(entity)) continue;

			var position = World.Get<Position>(entity);
			var sprite = World.Get<Sprite>(entity);

			Batcher.Image
			(
				Atlas.Get(Spritesheet.Sprites, sprite.Index),
				Camera.WorldToNative(position.Vector),
				Color.White
			);
		}
	}
}
