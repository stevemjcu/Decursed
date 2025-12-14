using Foster.Framework;
using YetAnotherEcs;
using static Decursed.Components;

namespace Decursed;

internal class Motion(World world) : System(world)
{
	public override void Update(Time time)
	{
		foreach (var id in World
			.View(new Filter().Include<Position, Velocity, Active>()))
		{
			var position = World.Get<Position>(id).Value;
			var velocity = World.Get<Velocity>(id).Value;

			if (World.Has<Grounded>(id))
			{
				velocity.X *= 0.75f;
			}

			if (World.Has<Gravity>(id))
			{
				velocity.Y += Config.Gravity * time.Delta;
				World.Remove<Grounded>(id);

				if (velocity.Y > 0)
				{
					World.Set<Falling>(id);
				}
			}

			velocity = velocity.Clamp(
				new(-Config.TerminalSpeed, -Config.TerminalSpeed),
				new(+Config.TerminalSpeed, +Config.TerminalSpeed));

			position += velocity * time.Delta;

			World.Set<Position>(id, new(position));
			World.Set<Velocity>(id, new(velocity));
		}
	}
}
