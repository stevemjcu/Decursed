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

	public Level(string path, Game game)
	{
		World = new();
		Factory = new(World);

		foreach (var it in Directory.EnumerateFiles(path)) Factory.CreateTemplate(it);
		Factory.CreateRootInstance();

		game.Graphics.Camera.Size = Config.LevelSize;
		Draw = new(World, game.Graphics, Factory.Layouts);
	}

	public void Dispose() => World.Dispose();

	public void Update() { }

	public void Render() => Draw.Render();
}