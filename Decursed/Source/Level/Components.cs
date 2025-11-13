using System.Numerics;

namespace Decursed.Source.Level;

internal static class Components
{
	#region Render

	// Can be rendered as a sprite.
	public record struct Sprite(Enum Type);

	// Can be rendered as a grid of sprites.
	public record struct Tilemap(Enum[,] Grid);

	#endregion

	#region Update

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

	#region Relation

	// Can be used to enter a room.
	public record struct Enters();

	// Can be used to exit a room.
	public record struct Exits();

	#endregion
}