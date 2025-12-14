using Foster.Framework;
using YetAnotherEcs;
using static Decursed.Components;

namespace Decursed;

internal class Input(World World, Controls Controls) : System(World)
{
	private static Filter Items = new Filter()
		.Include<Position, Hitbox, Portable, Active>();

	private int JumpFrame = 0;

	public override void Update(Time time)
	{
		var view = World.View<HeldBy>(new(Player));

		if (Controls.Interact.Pressed)
		{
			if (view.Count > 0)
			{
				World.Set(view[0], World.Get<Velocity>(Player));
				World.Remove<HeldBy>(view[0]);
			}
			else if (World.Has<Grounded>(Player))
			{
				if (TryGetOverlappingItem(Player, out var id))
				{
					World.Set<HeldBy>(id, new(Player));
				}
			}
		}

		var velocity = World.Get<Velocity>(Player).Value;
		velocity.X = Controls.Move.Value.X * Config.MoveSpeed;

		if (Controls.Jump.Pressed && World.Has<Grounded>(Player))
		{
			JumpFrame = view.Count == 0 ? Config.JumpFrames : Config.ReducedJumpFrames;
		}

		if (JumpFrame-- > 0)
		{
			velocity.Y = Config.JumpSpeed;
		}

		World.Set<Velocity>(Player, new(velocity));
	}

	private bool TryGetOverlappingItem(int id0, out int id1)
	{
		if (!TryGetClosestItem(id0, out id1))
		{
			return false;
		}

		var position0 = World.Get<Position>(id0).Value;
		var position1 = World.Get<Position>(id1).Value;

		var rect0 = World.Get<Hitbox>(id0).Value.Translate(position0);
		var rect1 = World.Get<Hitbox>(id1).Value.Translate(position1);

		return rect0.Overlaps(rect1);
	}

	private bool TryGetClosestItem(int id0, out int id1)
	{
		var position = World.Get<Position>(id0).Value;
		var best = (Id: -1, Distance: float.MaxValue);

		foreach (var it in World.View(Items))
		{
			if (id0 == it)
			{
				continue;
			}

			var distance = (World.Get<Position>(it).Value - position).Length();

			if (distance < best.Distance)
			{
				best = (it, distance);
			}
		}

		return (id1 = best.Id) != -1;
	}
}
