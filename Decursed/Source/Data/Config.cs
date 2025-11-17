using Foster.Framework;
using System.Numerics;

namespace Decursed.Source.Data;

internal enum Spritesheet { Player, Props, Tiles };

/// <summary>
/// Contains global configuration options.
/// </summary>
internal static class Config
{
	public static string Title = "Decursed";

	public static string ContentPath = @".\Content";
	public static string LevelsPath = Path.Combine(ContentPath, "Levels");
	public static string TexturesPath = Path.Combine(ContentPath, "Textures");

	public static int TargetFps = 60;
	public static int WindowScale = 5;

	public static Point2 NativeResolution = new(128, 128);
	public static Point2 TileResolution = new(8, 8);
	public static Point2 WindowResolution = NativeResolution * WindowScale;

	public static Vector2 UnitSize = new(1, 1);
	public static Point2 LevelSize = NativeResolution / TileResolution;
}
