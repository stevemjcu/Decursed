using Foster.Framework;
using System.Numerics;

namespace Decursed.Source;

internal enum Archetype { Player, Chest };

internal enum Spritesheet { Player, Props, Tiles };

/// <summary>
/// Contains global configuration options.
/// </summary>
internal static class Config
{
	public static string Title = "Decursed";
	public static string ContentPath = @".\Content";
	public static string LevelPath = Path.Combine(ContentPath, "Level");
	public static string TexturePath = Path.Combine(ContentPath, "Texture");

	public static Point2 NativeResolution = new(128, 128);
	public static Point2 TileSize = new(8, 8);
	public static int Scale = 5;
	public static Point2 WindowResolution = NativeResolution * Scale;

	public static Vector2 UnitSize = new(1, 1);
	public static Vector2 LevelSize = NativeResolution / TileSize;
}
