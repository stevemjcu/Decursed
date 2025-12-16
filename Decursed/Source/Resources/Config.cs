using Foster.Framework;
using System.Numerics;

namespace Decursed;

internal static class Config
{
	public const string Title = "Decursed";
	public const string ContentPath = @".\Content";

	public static bool DebugMode = false;

	public readonly static string LevelPath = Path.Combine(ContentPath, "Level");
	public readonly static string TexturePath = Path.Combine(ContentPath, "Texture");

	public const int TargetFps = 60;
	public const int WindowScale = 5;

	public readonly static Point2 NativeResolution = new(128, 128);
	public readonly static Point2 TileResolution = new(8, 8);
	public readonly static Vector2 UnitSize = new(1, 1);
	public readonly static Point2 LevelSize = NativeResolution / TileResolution;

	public readonly static Rect StandardBox = new(0, 0, 1, 1);
	public readonly static Rect ThinBox = new(0.125f, 0.125f, 0.75f, 0.875f);

	public const float MoveSpeed = 7;
	public const float JumpSpeed = 20.5f;
	public const int JumpFrames = 4;
	public const int ReducedJumpFrames = 1;
	public const float ThrowSpeed = 12;
	public const float Gravity = 160;

	// FIXME: Higher speeds cause tunneling
	// 30 tiles/sec = 0.5 tiles/frame
	public const float TerminalSpeed = 20;

	public readonly static Vector2 MinVelocity = new(-TerminalSpeed);
	public readonly static Vector2 MaxVelocity = new(+TerminalSpeed);
	public readonly static Vector2 HoldOffset = new(0, -0.3f);

	public readonly static Point2 Left = new(-1, 1);
	public readonly static Point2 Right = new(1, 1);

	public static Point2 WindowResolution => NativeResolution * WindowScale;

	public static TimeSpan TargetFrameTime => TimeSpan.FromSeconds(1) / TargetFps;

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
