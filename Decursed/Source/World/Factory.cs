using MoonTools.ECS;
using System.Numerics;
using static Decursed.Components;

namespace Decursed;

internal class Factory(World world) : IDisposable
{
	private readonly World World = world;

	// ID to template to layout
	private readonly Dictionary<int, Entity> Templates = [];
	private readonly Dictionary<Entity, string[,]> Layouts = [];

	public void Dispose() => World.Dispose();

	public Entity CreateTemplate(string path)
	{
		var id = int.Parse(Path.GetFileNameWithoutExtension(path));
		var layout = Utility.ParseCsv(path, Config.LevelSize);
		var template = World.CreateEntity(id.ToString());

		(Templates[id], Layouts[template]) = (template, layout);
		return template;
	}

	public Entity CreateInstance(Entity template, Entity current)
	{
		var instance = World.CreateEntity();
		var layout = Layouts[template];

		for (var x = 0; x < layout.GetLength(0); x++)
		{
			for (var y = 0; y < layout.GetLength(1); y++)
			{
				var position = new Vector2(x, y);
				var value = layout[x, y];

				// TODO: Handle capitals as globals
				if (value[0] == '-' || value[0] == 'w') continue;

				var entity = value[0] switch
				{
					'r' => CreateRift(position, current),
					'c' => CreateChest(position, Templates[value[1]]),
					'b' => CreateBox(position),
					'k' => CreateKey(position),
					_ => throw new Exception($"Invalid entity: {value[0]}"),
				};

				World.Relate<ChildOf>(entity, instance, new());
			}
		}

		return instance;
	}

	private Entity CreateActor(Vector2 position)
	{
		var entity = World.CreateEntity();
		World.Set<Bounds>(entity, new(Config.UnitSize));
		World.Set<Position>(entity, new(position));
		World.Set<Velocity>(entity, new(Vector2.Zero));
		World.Set<Gravity>(entity, new());
		return entity;
	}

	public Entity CreatePlayer(Vector2 position)
	{
		var entity = CreateActor(position);
		World.Set<Receiver>(entity, new());
		return entity;
	}

	public Entity CreateChest(Vector2 position, Entity template)
	{
		var entity = CreateActor(position);
		World.Relate<EntersTo>(entity, template, new());
		return entity;
	}

	public Entity CreateRift(Vector2 position, Entity instance)
	{
		var entity = CreateActor(position);
		World.Relate<ExitsTo>(entity, instance, new());
		return entity;
	}

	public Entity CreateBox(Vector2 position)
	{
		var entity = CreateActor(position);
		World.Set<Platform>(entity, new());
		return entity;
	}

	public Entity CreateKey(Vector2 position)
	{
		var entity = CreateActor(position);
		World.Set<Unlock>(entity, new());
		return entity;
	}
}
