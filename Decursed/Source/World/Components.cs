using Foster.Framework;
using System.Numerics;
using YetAnotherEcs;

namespace Decursed;

internal static class Components
{
	// Can be looked up.
	[Indexed] public record struct Tag(char Value);

	// Can be instantiated.
	public record struct Layout(string[,] Value);

	// Can be composed from tiles.
	public record struct Tilemap(int[,] Value);

	// Can be drawn.
	public record struct Sprite(int Value);

	// Is facing a direction.
	public record struct Orientation(Point2 Value);

	// Can be positioned.
	public record struct Position(Vector2 Value);

	// Can move.
	public record struct Velocity(Vector2 Value);

	// Can fall.
	public record struct Gravity();

	// Is grounded.
	public record struct Grounded();

	// Is falling.
	public record struct Falling();

	// Can collide.
	public record struct Hitbox(Rect Value);

	// Can receive input.
	public record struct Receiver();

	// Can be held and thrown.
	public record struct Portable();

	// Can be stood on.
	public record struct Platform();

	// Can unlock doors.
	public record struct Unlock();

	// Can end the level.
	public record struct Goal();

	// Can be operated on.
	public record struct Active();

	// Is an entrance to a template.
	[Indexed] public record struct EntranceTo(Entity Value);

	// Is an exit to an entrance.
	[Indexed] public record struct ExitTo(Entity Value);

	// Is an instance of a template.
	[Indexed] public record struct InstanceOf(Entity Value);

	// Is a child of an entity.
	[Indexed] public record struct ChildOf(Entity Value);

	// Is held by an entity.
	[Indexed] public record struct HeldBy(Entity Value);
}
