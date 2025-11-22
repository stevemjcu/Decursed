using System.Numerics;

namespace Decursed;

internal static class Components
{
	#region Actor

	// Exists in the world.
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

	#endregion

	#region Room

	// Has a physical layout.
	public record struct Layout(int[,] Grid);

	#endregion

	#region Relation

	// Can be used to enter a room.
	public record struct Enters();

	// Can be used to exit a room.
	public record struct Exits();

	#endregion
}