using Decursed.Source.General;
using Flecs.NET.Core;
using Foster.Framework;
using static Decursed.Source.Level.Entities.Components;

namespace Decursed.Source.Level.Entities;

internal class Factory : IDisposable
{
	private readonly World World;

	public readonly Entity Player;
	public readonly Entity Rift;
	public readonly Entity Chest;
	public readonly Entity Box;

	public readonly Dictionary<int, Entity> Templates = [];

	public Factory(World world)
	{
		World = world;

		var actor = World.Prefab()
			.Set<Bounds>(new(Config.UnitSize))
			.Add<Position>()
			.Add<Velocity>()
			.Add<Gravity>();

		Player = World.Entity().IsA(actor).Add<Receiver>();
		Rift = World.Entity().IsA(actor);
		Chest = World.Entity().IsA(actor).Add<Portable>();
		Box = World.Entity().IsA(actor).Add<Portable>();
	}

	public void Dispose() => World.Dispose();

	public Entity RegisterTemplate(string path)
	{
		var id = int.Parse(Path.GetFileNameWithoutExtension(path));
		var content = Utility.ParseCsv(path, Config.LevelSize);

		var layout = new int[Config.LevelSize.X, Config.LevelSize.Y];
		var template = World.Prefab().Set<Layout>(new(layout));

		for (var x = 0; x < Config.LevelSize.X; x++)
		{
			for (var y = 0; y < Config.LevelSize.Y; y++)
			{
				var value = content[x, y];

				if (value == string.Empty) continue;
				if (value[0] == 'w') { layout[x, y] = 1; continue; }

				var entity = value[0] switch
				{
					'r' => World.Entity().IsA(Rift),
					'c' => World.Entity().IsA(Chest),
					'b' => World.Entity().IsA(Box),
					_ => default
				};

				entity.ChildOf(template);
			}
		}

		Templates[id] = template;
		return template;
	}
}
