using System.Numerics;

namespace Decursed;

internal static class Components
{
	// Can be looked up.
	public record struct Tag(char Value);

	// Can be drawn.
	public record struct Sprite(int Index);

	// Can be drawn with multiple tiles.
	public record struct Tilemap(string[,] Value);

	// Can be animated.
	public record struct Animation(int Index, int Frame);

	// Can be positioned.
	public record struct Position(Vector2 Value);

	// Can move.
	public record struct Velocity(Vector2 Value);

	// Can fall.
	public record struct Gravity();

	// Can collide.
	public record struct Bounds(Vector2 Value);

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

	// Is an entrance to a template.
	public record struct EntersTo(int Id);

	// Is an exit to an entrance.
	public record struct ExitsTo(int Id);

	// Is an instance of a template.
	public record struct InstanceOf(int Id);

	// Is a child of an entity.
	public record struct ChildOf(int Id);
}
