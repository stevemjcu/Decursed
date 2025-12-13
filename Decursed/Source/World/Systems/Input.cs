using Foster.Framework;
using YetAnotherEcs;
using static Decursed.Components;

namespace Decursed;

internal class Input(World World, Controls Controls) : System(World)
{
	public override void Update(Time time)
	{
		var move = Controls.Move.Value.X;
		var jump = Controls.Jump.Pressed;

		var velocity = World.Get<Velocity>(Player).Value;
		velocity.X = move * Config.MoveSpeed;

		if (jump && World.Has<Grounded>(Player))
		{
			velocity.Y = Config.JumpImpulse;
		}

		World.Set<Velocity>(Player, new(velocity));
	}
}
