using MoonTools.ECS;
using System.Numerics;
using static Decursed.Components;

namespace Decursed;

internal class Factory(World world) : IDisposable
{
	private readonly World World = world;

	// Maps tag to template to layout
	private readonly Dictionary<char, Entity> Templates = [];
	private readonly Dictionary<Entity, string[,]> Layouts = [];

	public void Dispose() => World.Dispose();

	public Entity CreateTemplate(string path)
	{
		var tag = char.Parse(Path.GetFileNameWithoutExtension(path));
		var layout = Utility.ParseCsv(path, Config.LevelSize);
		var template = World.CreateEntity();

		(Templates[tag], Layouts[template]) = (template, layout);
		return template;
	}

	public Entity CreateRootInstance() =>
		CreateInstanceInternal(Templates['0'], null);

	public Entity CreateInstance(Entity template, Entity current) =>
		CreateInstanceInternal(template, current);

	private Entity CreateInstanceInternal(Entity template, Entity? entrance)
	{
		var instance = World.CreateEntity();
		var layout = Layouts[template];

		for (var x = 0; x < layout.GetLength(0); x++)
		{
			for (var y = 0; y < layout.GetLength(1); y++)
			{
				var position = new Vector2(x, y);
				var value = layout[x, y];

				if (value[0] == '-' || value[0] == 'w') continue;

				var entity = value[0] switch
				{
					'R' or 'r' => entrance == null ? CreatePlayer(position) : CreateRift(position, (Entity)entrance),
					'C' or 'c' => CreateChest(position, Templates[value[1]]),
					'B' or 'b' => CreateBox(position),
					'K' or 'k' => CreateKey(position),
					_ => throw new Exception($"Invalid entity: {value[0]}"),
				};

				World.Relate<ChildOf>(entity, instance, new());
			}
		}

		return instance;
	}

	private Entity CreatePlayer(Vector2 position)
	{
		var entity = CreateActor(position);
		World.Set<Receiver>(entity, new());
		return entity;
	}

	private Entity CreateRift(Vector2 position, Entity entrance)
	{
		var entity = CreateActor(position);
		World.Relate<ExitsTo>(entity, entrance, new());
		return entity;
	}

	private Entity CreateChest(Vector2 position, Entity template)
	{
		var entity = CreateActor(position);
		World.Relate<EntersTo>(entity, template, new());
		return entity;
	}

	private Entity CreateBox(Vector2 position)
	{
		var entity = CreateActor(position);
		World.Set<Platform>(entity, new());
		return entity;
	}

	private Entity CreateKey(Vector2 position)
	{
		var entity = CreateActor(position);
		World.Set<Unlock>(entity, new());
		return entity;
	}

	private Entity CreateActor(Vector2 position, int sprite = 0)
	{
		var entity = World.CreateEntity();
		World.Set<Sprite>(entity, new(sprite));
		World.Set<Position>(entity, new(position));
		World.Set<Velocity>(entity, new(Vector2.Zero));
		World.Set<Gravity>(entity, new());
		World.Set<Bounds>(entity, new(Config.UnitSize));
		return entity;
	}
}
