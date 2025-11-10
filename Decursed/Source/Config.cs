using Foster.Framework;

namespace Decursed.Source;

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
	public static int Scale = 4;

	public static Point2 WindowResolution => NativeResolution * Scale;
}
