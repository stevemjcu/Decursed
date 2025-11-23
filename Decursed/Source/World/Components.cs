using System.Numerics;

namespace Decursed;

internal static class Components
{
	#region Capabilities

	// Can be positioned.
	public record struct Position(Vector2 Vector);

	// Can move.
	public record struct Velocity(Vector2 Vector);

	// Can collide.
	public record struct Bounds(Vector2 Vector);

	// Can fall.
	public record struct Gravity();

	// Can receive input.
	public record struct Receiver();

	// Can be held and thrown.
	public record struct Portable();

	// Can be stood on.
	public record struct Platform();

	// Can unlock things.
	public record struct Unlock();

	// Can end the level.
	public record struct Goal();

	#endregion

	#region Relations

	// Is an entrance to a room.
	public record struct EntersTo();

	// Is an exit to a room.
	public record struct ExitsTo();

	// Is an instance of a template.
	public record struct InstanceOf();

	// Is a child of an entity.
	public record struct ChildOf();

	#endregion
}