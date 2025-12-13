using Foster.Framework;
using YetAnotherEcs;

namespace Decursed;

internal class Level : IScene, IDisposable
{
	private readonly World World = new();
	private readonly Factory Factory;

	private readonly List<System> UpdateSystems;
	private readonly List<System> RenderSystems;

	public Level(string path, Game game)
	{
		Factory = new(World);
		Factory.LoadLevel(path);

		UpdateSystems =
		[
			new Input(World, game.Controls),
			new Motion(World),
			new Collision(World)
		];

		RenderSystems =
		[
			new Render(World, game.Graphics),
		];
	}

	void IDisposable.Dispose() { }

	public void Update(Time time)
	{
		foreach (var it in UpdateSystems)
		{
			it.Update(time);
		}
	}

	public void Render(Time time)
	{
		foreach (var it in RenderSystems)
		{
			it.Update(time);
		}
	}
}
