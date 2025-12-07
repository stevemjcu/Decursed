using Foster.Framework;
using YetAnotherEcs;
using static Decursed.Components;

namespace Decursed;

internal class Motion(World world) : System(world)
{
	public override void Tick(Time time)
	{
		// Apply velocities to positions and resolve collisions

		var view = World.View(new Filter().Include<Position>().Include<Velocity>());

		foreach (var it in view)
		{
			var position = World.Get<Position>(it);
			var velocity = World.Get<Velocity>(it);

			position.Value += velocity.Value * time.Delta;
			World.Set(it, position);
		}
	}
}
