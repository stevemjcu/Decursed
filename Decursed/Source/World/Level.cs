using MoonTools.ECS;

namespace Decursed;

/// <summary>
/// Manages the level and its ECS.
/// </summary>
internal class Level : IScene, IDisposable
{
	private readonly World World = new();

	private readonly List<System> UpdateSystems;
	private readonly List<System> RenderSystems;

	public Level(string path, Resources resources)
	{
		var factory = new Factory(World);
		foreach (var it in Directory.EnumerateFiles(path)) factory.CreateTemplate(it);
		factory.CreateRootInstance();

		UpdateSystems =
		[
			new Input(World),
			new Motion(World),
		];

		RenderSystems =
		[
			new Draw(World, factory, resources),
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