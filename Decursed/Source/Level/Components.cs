using Arch.Core;
using Foster.Framework;
using System.Numerics;

namespace Decursed.Source.Level;

internal static class Components
{
	// Can be rendered.
	public record struct Sprite(Subtexture Texture);

	// Can be moved.
	public record struct Body(Vector2 Position, Vector2 Velocity);

	// Can fall.
	public record struct Gravity(bool Enabled);

	// Can collide.
	public record struct Collider(Rect Bounds);

	// Can receive input.
	public record struct Receiver();

	// Can be held and thrown.
	public record struct Item();

	// Can hold and throw an item.
	public record struct Hold(Entity Item);

	// Can be entered to activate.
	public record struct Entrance(Action Callback);
}