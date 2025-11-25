using MoonTools.ECS;

namespace Decursed;

/// <summary>
/// Manages the level and its ECS.
/// </summary>
internal class Level : IScene, IDisposable
{
	private readonly World World;
	private readonly Factory Factory;

	private readonly List<System> UpdateSystems;
	private readonly List<System> RenderSystems;

	public Level(string path)
	{
		World = new();
		Factory = new(World);

		foreach (var it in Directory.EnumerateFiles(path)) Factory.CreateTemplate(it);
		Factory.CreateRootInstance();

		UpdateSystems =
		[
			new Input(World),
			new Motion(World),
		];

		RenderSystems =
		[
			new Draw(World, Factory),
		];
	}

	public void Dispose() => World.Dispose();

	public void Update()
	{
		foreach (var it in UpdateSystems) it.Update();
	}

	public void Render()
	{
		foreach (var it in RenderSystems) it.Update();
	}
}