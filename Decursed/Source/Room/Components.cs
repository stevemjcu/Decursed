using Arch.Core;
using Foster.Framework;
using System.Numerics;

namespace Decursed.Source.Room;

// Can be rendered.
public record struct Sprite(Subtexture Texture);

// Can be moved.
public record struct Body(Vector2 Position, Vector2 Velocity, bool HasGravity);

// Can collide.
public record struct Collide(Rect Bounds);

// Can receive input.
public record struct Input();

// Can be held and thrown.
public record struct Item();

// Can hold and throw an item.
public record struct Hold(Entity Item);

// Can be interacted with.
public record struct Interact(Action Callback);