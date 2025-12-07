using YetAnotherEcs;

namespace Decursed;

internal class Level : IScene, IDisposable
{
	private readonly World World = new();
	private readonly Factory Factory;

	private readonly List<System> UpdateSystems;
	private readonly List<System> RenderSystems;

	public Level(string path, Graphics graphics)
	{
		Factory = new Factory(World);

		foreach (var it in Directory.EnumerateFiles(path))
		{
			Factory.CreateTemplate(it);
		}

		Factory.CreateRootInstance();

		UpdateSystems =
		[
			new Input(World),
			new Motion(World),
		];

		RenderSystems =
		[
			new Draw(World, graphics),
		];
	}

	void IDisposable.Dispose() { }

	public void Update()
	{
		foreach (var it in UpdateSystems)
		{
			it.Update();
		}
	}

	public void Render()
	{
		foreach (var it in RenderSystems)
		{
			it.Update();
		}
	}
}
