using Foster.Framework;
using YetAnotherEcs;
using static Decursed.Components;

namespace Decursed;

internal class Input(World World, Controls Controls) : System(World)
{
	private int JumpFrame = 0;

	public override void Update(Time time)
	{
		var move = Controls.Move.Value.X;
		var jump = Controls.Jump.Pressed;

		var velocity = World.Get<Velocity>(Player).Value;
		velocity.X = move * Config.MoveSpeed;

		if (jump && World.Has<Grounded>(Player))
		{
			JumpFrame = 3;
		}

		if (JumpFrame-- > 0)
		{
			velocity.Y = Config.JumpSpeed;
		}

		World.Set<Velocity>(Player, new(velocity));
	}
}
