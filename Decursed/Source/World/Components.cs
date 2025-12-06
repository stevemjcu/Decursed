using System.Numerics;

namespace Decursed;

/// <summary>
/// Contains all components which can belong to an entity.
/// </summary>
internal static class Components
{
	#region Capabilities

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

	#endregion

	#region Relations

	// Is an entrance to a template.
	public record struct EntersTo();

	// Is an exit to an entrance.
	public record struct ExitsTo();

	// Is an instance of a template.
	public record struct InstanceOf();

	// Is a child of an entity.
	public record struct ChildOf();

	#endregion
}
