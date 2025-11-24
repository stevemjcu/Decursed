using MoonTools.ECS;
using System.Numerics;
using static Decursed.Components;

namespace Decursed;

internal class Draw
(
	World world,
	Graphics graphics,
	Dictionary<Entity, string[,]> layouts
)
	: Renderer(world)
{
	public Graphics Graphics = graphics;
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
				var position = new Vector2(x, y);

				// TODO: Render tile
			}
		}

		foreach (var entity in World.InRelations<ChildOf>(instance))
		{
			if (!World.Has<Sprite>(entity)) continue;

			var position = World.Get<Position>(entity);
			var sprite = World.Get<Sprite>(entity);

			// TODO: Render sprite
		}
	}
}
