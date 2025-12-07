using System.Numerics;
using YetAnotherEcs;
using static Decursed.Components;

namespace Decursed;

internal class Factory(World World)
{
	public int CreateTemplate(string path)
	{
		var tag = char.Parse(Path.GetFileNameWithoutExtension(path));
		var layout = Utility.ParseCsv(path, Config.LevelSize);

		var template = World.Create();
		World.Set(template, new Tag(tag));
		World.Set(template, new Tilemap(layout));

		return template;
	}

	public int CreateRootInstance()
	{
		return CreateInstance(World.View(new Tag('0'))[0]);
	}

	public int CreateInstance(int template, int entrance = -1)
	{
		var instance = World.Create();
		World.Set(instance, new InstanceOf(template));

		var tilemap = World.Get<Tilemap>(template).Value;

		for (var x = 0; x < tilemap.GetLength(0); x++)
		{
			for (var y = 0; y < tilemap.GetLength(1); y++)
			{
				var position = new Vector2(x, y);
				var value = tilemap[x, y];
				var (archetype, tag) = (value[0], value[1]);

				if (archetype == '-' || archetype == 'w')
				{
					continue;
				}

				var entity = archetype switch
				{
					'R' or 'r' => entrance == -1 ? CreatePlayer(position) : CreateRift(position, entrance),
					'C' or 'c' => CreateChest(position, World.View(new Tag(tag))[0]),
					'B' or 'b' => CreateBox(position),
					'K' or 'k' => CreateKey(position),
					'G' or 'g' => CreateGem(position),
					_ => throw new Exception($"Invalid entity: {archetype}"),
				};

				World.Set(entity, new ChildOf(instance));
			}
		}

		return instance;
	}

	private int CreatePlayer(Vector2 position)
	{
		var entity = CreateActor(position, (int)Config.Actors.Player);
		World.Set(entity, new Receiver());
		return entity;
	}

	private int CreateRift(Vector2 position, int entrance)
	{
		var entity = CreateActor(position);
		World.Set(entity, new ExitsTo(entrance));
		return entity;
	}

	private int CreateChest(Vector2 position, int template)
	{
		var entity = CreateActor(position, (int)Config.Actors.ChestOpen);
		World.Set(entity, new EntersTo(template));
		return entity;
	}

	private int CreateBox(Vector2 position)
	{
		var entity = CreateActor(position, (int)Config.Actors.Box);
		World.Set(entity, new Platform());
		return entity;
	}

	private int CreateKey(Vector2 position)
	{
		var entity = CreateActor(position, (int)Config.Actors.Key);
		World.Set(entity, new Unlock());
		return entity;
	}

	private int CreateGem(Vector2 position)
	{
		var entity = CreateActor(position, (int)Config.Actors.Gem);
		World.Set(entity, new Unlock());
		return entity;
	}

	private int CreateActor(Vector2 position, int sprite = 0)
	{
		var entity = World.Create();
		World.Set(entity, new Sprite(sprite));
		World.Set(entity, new Position(position));
		World.Set(entity, new Velocity(Vector2.Zero));
		World.Set(entity, new Gravity());
		World.Set(entity, new Bounds(Config.UnitSize));
		return entity;
	}
}
