using Foster.Framework;

namespace Decursed;

/// <summary>
/// Contains global configuration options.
/// </summary>
internal static class Config
{
	public static string Title = "Decursed";
	public static Point2 NativeResolution = new(128, 128);
	public static Point2 DisplayResolution = NativeResolution * 3;
}
