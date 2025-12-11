using System.Numerics;
using YetAnotherEcs;

namespace Decursed;

internal static class Components
{
	// Can be looked up.
	[Indexed] public record struct Tag(char Value);

	// Can be instantiated.
	public record struct Layout(string[,] Map);

	// Can be composed from tiles.
	public record struct Tilemap(int[,] Map);

	// Can be drawn.
	public record struct Sprite(int Index);

	// Can be animated.
	public record struct Animation(int Index, int Frame);

	// Can be positioned.
	public record struct Position(Vector2 Vector);

	// Can move.
	public record struct Velocity(Vector2 Vector);

	// Can fall.
	public record struct Gravity();

	// Can collide.
	public record struct Bounds(Vector2 Vector);

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

	// Is an entrance for a template.
	[Indexed] public record struct EntranceFor(int Id);

	// Is an exit for an entrance.
	[Indexed] public record struct ExitFor(int Id);

	// Is an instance of a template.
	[Indexed] public record struct InstanceOf(int Id);

	// Is a child of an entity.
	[Indexed] public record struct ChildOf(int Id);
}
