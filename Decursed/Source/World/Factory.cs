using System.Numerics;
using YetAnotherEcs;
using static Decursed.Components;

namespace Decursed;

internal class Factory(World World)
{
	public void LoadLevel(string path)
	{
		foreach (var it in Directory.EnumerateFiles(path))
		{
			CreateTemplate(it);
		}

		CreateRootInstance();
	}

	public int CreateTemplate(string path)
	{
		var tag = char.Parse(Path.GetFileNameWithoutExtension(path));
		var layout = Utility.ParseCsv(path, Config.LevelSize);

		var id = World.Create();
		World.Set<Tag>(id, new(tag));
		World.Set<Layout>(id, new(layout));

		return id;
	}

	public int CreateRootInstance()
	{
		return CreateInstance(World.View(new Tag('0'))[0]);
	}

	public int CreateInstance(int template, int entrance = -1)
	{
		var layout = World.Get<Layout>(template).Value;
		var tilemap = new int[layout.GetLength(0), layout.GetLength(1)];

		var id = World.Create();
		World.Set<InstanceOf>(id, new(template));
		World.Set<Tilemap>(id, new(tilemap));

		for (var x = 0; x < layout.GetLength(0); x++)
		{
			for (var y = 0; y < layout.GetLength(1); y++)
			{
				var position = new Vector2(x, y);
				var value = layout[x, y];
				var (archetype, tag) = (value[0], value[1]);

				if (archetype == '-' || archetype == 'w')
				{
					tilemap[x, y] = archetype == 'w' ? 1 : 0;
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

				World.Set<ChildOf>(entity, new(id));
			}
		}

		return id;
	}

	private int CreatePlayer(Vector2 position)
	{
		var id = CreateActor(position, (int)Config.Actors.Player);
		World.Set<Receiver>(id);
		return id;
	}

	private int CreateRift(Vector2 position, int entrance)
	{
		var id = CreateActor(position);
		World.Set<ExitFor>(id, new(entrance));
		return id;
	}

	private int CreateChest(Vector2 position, int template)
	{
		var id = CreateActor(position, (int)Config.Actors.ChestOpen);
		World.Set<EntranceFor>(id, new(template));
		return id;
	}

	private int CreateBox(Vector2 position)
	{
		var id = CreateActor(position, (int)Config.Actors.Box);
		World.Set<Platform>(id);
		return id;
	}

	private int CreateKey(Vector2 position)
	{
		var id = CreateActor(position, (int)Config.Actors.Key);
		World.Set<Unlock>(id);
		return id;
	}

	private int CreateGem(Vector2 position)
	{
		var id = CreateActor(position, (int)Config.Actors.Gem);
		World.Set<Goal>(id);
		return id;
	}

	private int CreateActor(Vector2 position, int sprite = 0)
	{
		var id = World.Create();
		World.Set<Sprite>(id, new(sprite));
		World.Set<Position>(id, new(position));
		World.Set<Velocity>(id, new(Vector2.Zero));
		World.Set<Hitbox>(id, new(Config.UnitSize));
		World.Set<Gravity>(id);
		World.Set<Active>(id);
		return id;
	}
}
