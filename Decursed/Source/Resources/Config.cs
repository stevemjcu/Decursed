using Foster.Framework;
using System.Numerics;

namespace Decursed;

internal static class Config
{
	public const string Title = "Decursed";
	public const string ContentPath = @".\Content";

	public readonly static string LevelPath = Path.Combine(ContentPath, "Level");
	public readonly static string TexturePath = Path.Combine(ContentPath, "Texture");

	public const int TargetFps = 60;
	public readonly static TimeSpan TargetFrameTime = TimeSpan.FromSeconds(1) / TargetFps;
	public const int WindowScale = 5;

	public readonly static Point2 NativeResolution = new(128, 128);
	public readonly static Point2 TileResolution = new(8, 8);
	public readonly static Vector2 UnitSize = new(1, 1);
	public readonly static Point2 LevelSize = NativeResolution / TileResolution;

	public readonly static Rect StandardBox = new(0, 0, 1, 1);
	public readonly static Rect ThinBox = new(0.125f, 0.125f, 0.75f, 0.875f);

	public const float MoveSpeed = 8;
	public const float JumpSpeed = -25;
	public const int JumpFrames = 5;
	public const int ReducedJumpFrames = 3;
	public const float ThrowSpeed = 12;
	public const float Gravity = 120;

	// FIXME: Higher speeds cause tunneling
	// 30 tiles/sec = 0.5 tiles/frame
	public const float TerminalSpeed = 20;

	public readonly static Vector2 MinVelocity = new(-TerminalSpeed);
	public readonly static Vector2 MaxVelocity = new(+TerminalSpeed);
	public readonly static Vector2 HoldOffset = new(0, 0.25f);

	public static Point2 WindowResolution => NativeResolution * WindowScale;

	public enum Spritesheet { Actors, Tiles };

	public enum Actors
	{
		Unknown,
		Cursor,
		ChestOpen,
		ChestClosed,
		Key,
		Box,
		Gem,
		Urn,
		Player
	}

	public static readonly List<Point2> Directions =
	[
		new(-1, -1), new(+0, -1), new(+1, -1),
		new(-1, +0), new(+0, +0), new(+1, +0),
		new(-1, +1), new(+0, +1), new(+1, +1)
	];
}
