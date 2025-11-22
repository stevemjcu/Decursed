using MoonTools.ECS;
using static Decursed.Components;

namespace Decursed;

internal class Factory(World world) : IDisposable
{
	private readonly World World = world;
	private readonly Dictionary<int, Entity> Templates = [];

    public void Dispose() => World.Dispose();

	public void CreateTemplate(string path)
	{
		var id = int.Parse(Path.GetFileNameWithoutExtension(path));
		var content = Utility.ParseCsv(path, Config.LevelSize);

		var layout = new int[Config.LevelSize.X, Config.LevelSize.Y];
		var template = World.CreateEntity();

		World.Set<Layout>(template, new(layout));

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

	public Entity CreateRoom(int id) => default;

	public Entity CreatePlayer() => default;

	public Entity CreateChest() => default;
}
