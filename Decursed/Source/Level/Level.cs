using Decursed.Source.General;
using Decursed.Source.Level.Entities;
using Flecs.NET.Core;

namespace Decursed.Source.Level;

/// <summary>
/// Manages the call stack and topmost room.
/// </summary>
internal class Level : IScene, IDisposable
{
	private readonly World World = World.Create();
	private readonly Factory Factory;

	public Level(string path)
	{
		Factory = new(World);

		foreach (var it in Directory.EnumerateFiles(path)) Factory.RegisterTemplate(it);

		World.Entity().IsA(Factory.Player);
	}

	public void Dispose() => World.Dispose();

	public void Update() { }

	public void Render()
	{
		// TODO: Render room parented to player
	}
}