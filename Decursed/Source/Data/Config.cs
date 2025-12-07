using Foster.Framework;
using System.Numerics;

namespace Decursed;

internal static class Config
{
	public const string Title = "Decursed";
	public const string ContentPath = @".\Content";

	public readonly static string LevelPath = Path.Combine(ContentPath, "Level");
	public readonly static string TexturePath = Path.Combine(ContentPath, "Texture");

	public static int TargetFps = 60;
	public static int WindowScale = 5;

	public readonly static Point2 NativeResolution = new(128, 128);
	public readonly static Point2 TileResolution = new(8, 8);
	public readonly static Vector2 UnitSize = new(1, 1);
	public readonly static Point2 LevelSize = NativeResolution / TileResolution;

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
}
