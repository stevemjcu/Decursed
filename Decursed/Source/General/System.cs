using MoonTools.ECS;

namespace Decursed;

internal abstract class System(World world)
{
	protected readonly World World = world;

	public abstract void Update();
}
