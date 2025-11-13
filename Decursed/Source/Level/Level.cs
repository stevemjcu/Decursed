using Decursed.Source.General;
using Flecs.NET.Core;
using Foster.Framework;

namespace Decursed.Source.Level;

/// <summary>
/// Manages the call stack and topmost room.
/// </summary>
internal class Level : IScene, IDisposable
{
	private readonly World World = World.Create();
	private readonly Factory Factory;
	private readonly Entity Player;

	public Level(string path)
	{
		Factory = new(World);
		Player = Factory.CreateActor(Archetype.Player);

		foreach (var it in Directory.EnumerateFiles(path))
		{
			var id = int.Parse(Path.GetFileNameWithoutExtension(it));
			var layout = Utility.ParseCsv(it, (Point2)Config.LevelSize);
			Factory.CreateTemplate(id, layout);
		}

		// TODO: Instantiate root template as room parented to player
	}

	public void Dispose() => World.Dispose();

	public void Update() { }

	public void Render()
	{
		// TODO: Render room parented to player
	}
}