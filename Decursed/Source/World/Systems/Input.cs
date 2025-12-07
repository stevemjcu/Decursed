using Foster.Framework;
using YetAnotherEcs;
using static Decursed.Components;

namespace Decursed;

internal class Input(World World, Controls Controls) : System(World)
{
	public override void Tick(Time time)
	{
		// Apply velocity to player in net direction of input

		var direction = Controls.Move.Value; // unit vector?
		var velocity = direction * Config.MoveSpeed;

		//Console.WriteLine(velocity);

		World.Set<Velocity>(Player, new(velocity));
	}
}
