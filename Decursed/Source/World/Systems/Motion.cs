using Foster.Framework;
using YetAnotherEcs;
using static Decursed.Components;

namespace Decursed;

internal class Motion(World world) : System(world) {
	public override void Update(Time time) {
		foreach (var it in World.View(new Filter().Include<Position, Velocity, Focused>())) {
			var position = it.Get<Position>().Value;
			var velocity = it.Get<Velocity>().Value;

			if (it.Has<Grounded>() && it != Player) {
				velocity.X *= Config.Friction;
			}

			if (it.Has<Gravity>()) {
				velocity.Y += Config.Gravity * time.Delta;
				it.Remove<Grounded>();
			}
			else {
				it.Remove<Grounded>();
			}

			velocity = velocity.Clamp(Config.MinVelocity, Config.MaxVelocity);
			position += velocity * time.Delta;

			it.Set<Position>(new(position));
			it.Set<Velocity>(new(velocity));
		}
	}
}
