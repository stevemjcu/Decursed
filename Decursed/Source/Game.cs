using Foster.Framework;
using System.Numerics;

namespace Decursed;

/// <summary>
/// Manages the game loop.
/// </summary>
internal class Game : App
{
#pragma warning disable CS8618
	internal static Batcher Batcher;
	internal static Target Buffer;
	internal static Camera Camera;
	internal static Atlas Atlas;
#pragma warning restore CS8618

	internal readonly Stack<IScene> Scenes = [];

	public Game() : base
	(
		Config.Title,
		Config.WindowResolution.X,
		Config.WindowResolution.Y
	)
	{
		Batcher = new(GraphicsDevice);
		Buffer = new(GraphicsDevice, Config.NativeResolution.X, Config.NativeResolution.Y);
		Camera = new(Config.NativeResolution, Config.WindowResolution, Config.LevelSize);
		Atlas = new(GraphicsDevice);

		foreach (var it in Directory.EnumerateFiles(Config.TexturePath)) Atlas.Add(it);
		Atlas.Pack();

		Scenes.Push(new Level(Path.Combine(Config.LevelPath, "00")));
	}

	protected override void Startup() => Window.SetMouseVisible(false);

	protected override void Shutdown() { }

	protected override void Update() => Scenes.Peek().Update();

	protected override void Render()
	{
		Window.Clear(Color.Black);
		Buffer.Clear(Color.Black);

		Scenes.Peek().Render();
		DrawCursor();

		// Render to buffer
		Batcher.Render(Buffer);
		Batcher.Clear();

		Batcher.PushSampler(new(TextureFilter.Nearest, TextureWrap.Clamp));
		Batcher.PushMatrix(Vector2.Zero, new(Config.WindowScale), 0);
		Batcher.Image(Buffer, Color.White);
		Batcher.PopMatrix();
		Batcher.PopSampler();

		// Render to window
		Batcher.Render(Window);
		Batcher.Clear();
	}

	private void DrawCursor()
	{
		var position = Camera.WindowToNative((Point2)Input.Mouse.Position);
		var subtexture = Atlas.Get(Config.Spritesheet.Actors, 1);
		Batcher.Image(subtexture, position, Color.White);
	}
}
