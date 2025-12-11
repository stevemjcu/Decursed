using Foster.Framework;
using YetAnotherEcs;
using static Decursed.Components;

namespace Decursed;

internal class Input(World World, Controls Controls) : System(World)
{
	public override void Tick(Time time)
	{
		var direction = Controls.Move.Value;
		var move = direction * Config.MoveSpeed;
		var jump = Controls.Jump.Pressed;

		var velocity = World.Get<Velocity>(Player).Vector with { X = move.X };

		if (jump)
		{
			velocity.Y -= Config.JumpImpulse;
		}

		World.Set(Player, new Velocity(velocity));
	}
}
