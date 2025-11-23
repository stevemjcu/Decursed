using MoonTools.ECS;

namespace Decursed;

/// <summary>
/// Manages the call stack and topmost room.
/// </summary>
internal class Level : IScene, IDisposable
{
	private readonly World World;
	private readonly Factory Factory;

	public Level(string path)
	{
		World = new();
		Factory = new(World);

		foreach (var it in Directory.EnumerateFiles(path)) Factory.CreateTemplate(it);
		Factory.CreateRootInstance();
	}

	public void Dispose() => World.Dispose();

	public void Update() { }

	public void Render()
	{
		// TODO: Render room parented to player
	}
}