using Foster.Framework;
using YetAnotherEcs;

namespace Decursed;

internal class Level : IScene {
	private readonly World World = new();
	private readonly Factory Factory;

	private readonly List<System> UpdateSystems;
	private readonly List<System> RenderSystems;
	private readonly List<System> OverlaySystems;

	public Level(string path, Game game) {
		Factory = new(World);
		Factory.LoadLevel(path);

		UpdateSystems = [
			new Input(World, game.Controls),
			new Motion(World),
			new Collision(World),
			new Stacking(World),
			new Holding(World)];

		RenderSystems = [new Render(World, game.Graphics)];
		OverlaySystems = [new Debug(World, game.Graphics)];
	}

	public void Update(Time time) {
		foreach (var it in UpdateSystems) {
			it.Update(time);
		}
	}

	public void RenderToBuffer(Time time) {
		foreach (var it in RenderSystems) {
			it.Update(time);
		}
	}

	public void RenderToScreen(Time time) {
		foreach (var it in OverlaySystems) {
			it.Update(time);
		}
	}
}
