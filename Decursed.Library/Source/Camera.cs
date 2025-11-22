using Foster.Framework;
using System.Numerics;

namespace Decursed.Library.Source;

/// <summary>
/// Converts between game and screen coordinates.
/// </summary>
public class Camera
{
	// World
	public Vector2 Position;
	public Vector2 Size;

	// Screen
	public Point2 NativeResolution;
	public Point2 WindowResolution;

	public Point2 WindowToNative(Point2 position) =>
		(Point2)((Vector2)position / WindowResolution * NativeResolution);

	public Vector2 NativeToWorld(Point2 position) =>
		position / WindowResolution * Size + Position;

	public Point2 WorldToNative(Vector2 position) =>
		(Point2)((position - Position) / Size * NativeResolution);
}
