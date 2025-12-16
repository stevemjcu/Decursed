using Foster.Framework;
using YetAnotherEcs;
using static Decursed.Components;

namespace Decursed;

internal class Input(World World, Controls Controls) : System(World)
{
	private static Filter Items = new Filter().Include<Position, Hitbox, Portable, Active>();

	private int JumpFrame = 0;

	public override void Update(Time time)
	{
		var view = World.View<HeldBy>(new(Player));

		if (Controls.Interact.Pressed)
		{
			if (view.Count > 0 && view[0] is var item)
			{
				item.Set(Player.Get<Velocity>());
				item.Remove<HeldBy>();
			}
			else if (Player.Has<Grounded>() && TryGetOverlappingItem(Player, out item))
			{
				item.Set<HeldBy>(new(Player));
				item.Remove<Grounded>();
			}
		}

		var velocity = Player.Get<Velocity>().Value;
		velocity.X = Controls.Move.Value.X * Config.MoveSpeed;

		if (Controls.Move.PressedLeft)
		{
			Player.Set<Orientation>(new(Config.Left));
		}
		else if (Controls.Move.PressedRight)
		{
			Player.Set<Orientation>(new(Config.Right));
		}

		if (Controls.Jump.Pressed && Player.Has<Grounded>())
		{
			JumpFrame = view.Count == 0 ? Config.JumpFrames : Config.ReducedJumpFrames;
		}

		if (JumpFrame-- > 0)
		{
			velocity.Y = -Config.JumpSpeed;
		}

		Player.Set<Velocity>(new(velocity));
	}

	private bool TryGetOverlappingItem(Entity a, out Entity b)
	{
		if (!TryGetClosestItem(a, out b))
		{
			return false;
		}

		var position0 = a.Get<Position>().Value;
		var position1 = b.Get<Position>().Value;

		var rect0 = a.Get<Hitbox>().Value.Translate(position0);
		var rect1 = b.Get<Hitbox>().Value.Translate(position1);

		return rect0.Overlaps(rect1);
	}

	private bool TryGetClosestItem(Entity a, out Entity b)
	{
		var position = a.Get<Position>().Value;
		var best = (Entity: (Entity?)null, Distance: float.MaxValue);

		foreach (var it in World.View(Items))
		{
			if (a == it)
			{
				continue;
			}

			var displacement = it.Get<Position>().Value - position;
			var distance = displacement.Length();

			if (distance < best.Distance)
			{
				best = (it, distance);
			}
		}

		b = best.Entity ?? default;
		return best.Entity is not null;
	}
}
