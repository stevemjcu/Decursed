using MoonTools.ECS;

namespace Decursed;

/// <summary>
/// Applies movement to entities and resolves collisions.
/// </summary>
internal class Motion(World world) : System(world)
{
	public override void Update() { }
}
