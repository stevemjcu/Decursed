using Foster.Framework;
using System.Numerics;
using YetAnotherEcs;
using static Decursed.Components;

namespace Decursed;

internal class Factory(World World) {
	public void LoadLevel(string path) {
		foreach (var it in Directory.EnumerateFiles(path)) {
			CreateTemplate(it);
		}

		CreateRootInstance();
	}

	public Entity CreateTemplate(string path) {
		var tag = char.Parse(Path.GetFileNameWithoutExtension(path));
		var layout = Utility.ParseCsv(path, Config.LevelSize);

		var template = World.Create();
		template.Set<Tag>(new(tag));
		template.Set<Layout>(new(layout));

		return template;
	}

	public Entity CreateRootInstance() {
		return CreateInstance(World.View<Tag>(new('0'))[0]);
	}

	public Entity CreateInstance(Entity template, Entity? entrance = null) {
		var layout = template.Get<Layout>().Value;
		var tilemap = new int[layout.GetLength(0), layout.GetLength(1)];

		var instance = World.Create();
		instance.Set<InstanceOf>(new(template));
		instance.Set<Tilemap>(new(tilemap));

		for (var x = 0; x < layout.GetLength(0); x++) {
			for (var y = 0; y < layout.GetLength(1); y++) {
				var position = new Vector2(x, y);
				var value = layout[x, y];
				var (archetype, tag) = (value[0], value[1]);

				if (archetype == '-' || archetype == 'w') {
					tilemap[x, y] = archetype == 'w' ? 1 : 0;
					continue;
				}

				var entity = archetype switch {
					'R' or 'r' => entrance is null ? CreatePlayer(position) : CreateRift(position, (Entity)entrance),
					'C' or 'c' => CreateChest(position, World.View<Tag>(new(tag))[0]),
					'B' or 'b' => CreateBox(position),
					'K' or 'k' => CreateKey(position),
					'G' or 'g' => CreateGem(position),
					_ => throw new Exception($"Invalid entity: {archetype}"),
				};

				entity.Set<ChildOf>(new(instance));
			}
		}

		return instance;
	}

	private Entity CreatePlayer(Vector2 position) {
		var entity = CreateActor(position, (int)Config.Actors.Player);
		entity.Set<Receiver>();
		entity.Set<Hitbox>(new(Config.ThinBox));
		return entity;
	}

	private Entity CreateRift(Vector2 position, Entity entrance) {
		var entity = CreateActor(position);
		entity.Set<ExitTo>(new(entrance));
		return entity;
	}

	private Entity CreateChest(Vector2 position, Entity template) {
		var entity = CreateActor(position, (int)Config.Actors.ChestOpen);
		entity.Set<EntranceTo>(new(template));
		entity.Set<Portable>();
		return entity;
	}

	private Entity CreateBox(Vector2 position) {
		var entity = CreateActor(position, (int)Config.Actors.Box);
		entity.Set<Platform>();
		entity.Set<Portable>();
		return entity;
	}

	private Entity CreateKey(Vector2 position) {
		var entity = CreateActor(position, (int)Config.Actors.Key);
		entity.Set<Unlock>();
		entity.Set<Portable>();
		return entity;
	}

	private Entity CreateGem(Vector2 position) {
		var entity = CreateActor(position, (int)Config.Actors.Gem);
		entity.Set<Hitbox>(new(Config.ThinBox));
		entity.Set<Goal>();
		entity.Remove<Velocity>();
		entity.Remove<Gravity>();
		return entity;
	}

	private Entity CreateActor(Vector2 position, int sprite = 0) {
		var entity = World.Create();
		entity.Set<Sprite>(new(sprite));
		entity.Set<Orientation>(new(Point2.One));
		entity.Set<Position>(new(position));
		entity.Set<Velocity>(new(Vector2.Zero));
		entity.Set<Hitbox>(new(Config.StandardBox));
		entity.Set<Gravity>();
		entity.Set<Focused>();
		return entity;
	}
}
