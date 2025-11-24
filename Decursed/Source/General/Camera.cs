using Foster.Framework;
using System.Numerics;

namespace Decursed;

/// <summary>
/// Converts between game and screen coordinates.
/// </summary>
internal class Camera(Point2 nativeResolution, Point2 windowResolution)
{
	// World
	public Vector2 Position;
	public Vector2 Size;

	// Screen
	public Point2 NativeResolution = nativeResolution;
	public Point2 WindowResolution = windowResolution;

	public Point2 WindowToNative(Point2 position) =>
		(Point2)((Vector2)position / WindowResolution * NativeResolution);

	public Vector2 NativeToWorld(Point2 position) =>
		position / WindowResolution * Size + Position;

	public Point2 WorldToNative(Vector2 position) =>
		(Point2)((position - Position) / Size * NativeResolution);
}
