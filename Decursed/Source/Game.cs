using Foster.Framework;
using System.Numerics;

namespace Decursed;

/// <summary>
/// Manages the game loop.
/// </summary>
internal class Game : App
{
	internal readonly Batcher Batcher;
	internal readonly Target Buffer;
	internal readonly Camera Camera;

	internal readonly Atlas Atlas = new();
	internal readonly Stack<IScene> Scenes = new();

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

		foreach (var it in Directory.EnumerateFiles(Config.TexturePath)) Atlas.Add(it);
		Atlas.Pack(GraphicsDevice);

		Scenes.Push(new Level(Path.Combine(Config.LevelPath, "00"), this));
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
		var subtexture = Atlas.Get(Spritesheet.Sprites, 1);
		Batcher.Image(subtexture, position, Color.White);
	}
}
