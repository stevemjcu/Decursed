using Decursed.Source.General;
using Flecs.NET.Core;

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
			var layout = Utility.ParseCsv(it, Config.LevelSize);
			Factory.CreateTemplate(id, layout);
		}

		Enter(Factory.CreateRoom(0));
	}

	public void Dispose() => World.Dispose();

	public void Update() { }

	public void Render()
	{
		// TODO: Render room parented to player
	}

	private void Enter(Entity room)
	{
		Player.ChildOf(room);
		// Rifts must be assigned to previous room
		// If no previous room, delete them
	}
}