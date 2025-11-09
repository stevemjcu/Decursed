using Foster.Framework;

namespace Decursed.Source;

/// <summary>
/// Contains global configuration options.
/// </summary>
internal static class Config
{
	public static string Title = "Decursed";
	public static Point2 NativeResolution = new(128, 128);
	public static int ScreenScale = 3;

	public static Point2 DisplayResolution => NativeResolution * ScreenScale;
}
