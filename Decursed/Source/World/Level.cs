using MoonTools.ECS;

namespace Decursed;

/// <summary>
/// Manages the level and its ECS.
/// </summary>
internal class Level : IScene, IDisposable
{
	private readonly World World;
	private readonly Factory Factory;

	// Systems
	private readonly Draw Draw;

	public Level(string path)
	{
		World = new();
		Factory = new(World);

		foreach (var it in Directory.EnumerateFiles(path)) Factory.CreateTemplate(it);
		Factory.CreateRootInstance();

		Draw = new(World, Factory);
	}

	public void Dispose() => World.Dispose();

	public void Update() { }

	public void Render() => Draw.Render();
}