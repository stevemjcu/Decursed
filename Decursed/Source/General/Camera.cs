using Foster.Framework;
using System.Numerics;

namespace Decursed;

internal class Camera(Point2 windowResolution, Point2 nativeResolution, Vector2 size)
{
	// World
	public Vector2 Position = Vector2.Zero;
	public Vector2 Size = size;

	// Screen
	public Point2 WindowResolution = windowResolution;
	public Point2 NativeResolution = nativeResolution;

	public Point2 WindowToNative(Point2 position)
	{
		return (Point2)((Vector2)position / WindowResolution * NativeResolution);
	}

	public Vector2 NativeToWorld(Point2 position)
	{
		return position / WindowResolution * Size + Position;
	}

	public Point2 WorldToNative(Vector2 position)
	{
		return ((position - Position) / Size * NativeResolution).RoundToPoint2();
	}

	public Point2 WorldToWindow(Vector2 position)
	{
		return ((position - Position) / Size * WindowResolution).RoundToPoint2();
	}
}
