using MoonTools.ECS;

namespace Decursed;

/// <summary>
/// Reads and/or writes to the world.
/// </summary>
internal abstract class System(World world)
{
	protected readonly World World = world;

	public abstract void Update();
}
