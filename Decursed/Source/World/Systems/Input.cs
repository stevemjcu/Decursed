using Foster.Framework;
using YetAnotherEcs;
using static Decursed.Components;

namespace Decursed;

internal class Input(World World, Controls Controls) : System(World)
{
	public override void Tick(Time time)
	{
		var direction = Controls.Move.Value;
		var velocity = direction * Config.MoveSpeed;

		World.Set<Velocity>(Player, new(velocity));
	}
}
