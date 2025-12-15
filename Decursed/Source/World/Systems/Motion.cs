using Foster.Framework;
using YetAnotherEcs;
using static Decursed.Components;

namespace Decursed;

internal class Motion(World world) : System(world)
{
	private static Filter Movable = new Filter().Include<Position, Velocity, Active>();

	public override void Update(Time time)
	{
		foreach (var it in World.View(Movable))
		{
			var position = it.Get<Position>().Value;
			var velocity = it.Get<Velocity>().Value;

			if (it.Has<Grounded>() && it != Player)
			{
				velocity.X *= 0.75f;
			}

			if (it.Has<Gravity>())
			{
				velocity.Y += Config.Gravity * time.Delta;
				it.Remove<Grounded>();

				if (velocity.Y > 0)
				{
					it.Set<Falling>();
				}
			}

			velocity = velocity.Clamp(Config.MinVelocity, Config.MaxVelocity);
			position += velocity * time.Delta;

			it.Set<Position>(new(position));
			it.Set<Velocity>(new(velocity));
		}
	}
}
