using Foster.Framework;
using System.Numerics;

namespace Decursed;

/// <summary>
/// Manages the game loop.
/// </summary>
internal class Game : App
{
	public readonly Graphics Graphics;
	public readonly Stack<IScene> Scenes = new();

	public Game() : base
	(
		Config.Title,
		Config.WindowResolution.X,
		Config.WindowResolution.Y
	)
	{
		var atlas = new Atlas();
		foreach (var it in Directory.EnumerateFiles(Config.TexturePath)) atlas.Add(it);
		atlas.Pack(GraphicsDevice);

		Graphics = new()
		{
			Batcher = new(GraphicsDevice),
			Buffer = new(GraphicsDevice, Config.NativeResolution.X, Config.NativeResolution.Y),
			Camera = new(Config.NativeResolution, Config.WindowResolution),
			Atlas = atlas
		};

		Scenes.Push(new Level(Path.Combine(Config.LevelPath, "00"), this));
	}

	protected override void Startup() => Window.SetMouseVisible(false);

	protected override void Shutdown() { }

	protected override void Update() => Scenes.Peek().Update();

	protected override void Render()
	{
		Window.Clear(Color.Black);
		Graphics.Buffer.Clear(Color.Black);

		Scenes.Peek().Render();
		DrawCursor();

		// Render to buffer
		Graphics.Batcher.Render(Graphics.Buffer);
		Graphics.Batcher.Clear();

		Graphics.Batcher.PushSampler(new(TextureFilter.Nearest, TextureWrap.Clamp));
		Graphics.Batcher.PushMatrix(Vector2.Zero, new(Config.WindowScale), 0);
		Graphics.Batcher.Image(Graphics.Buffer, Color.White);
		Graphics.Batcher.PopMatrix();
		Graphics.Batcher.PopSampler();

		// Render to window
		Graphics.Batcher.Render(Window);
		Graphics.Batcher.Clear();
	}

	private void DrawCursor()
	{
		var position = Graphics.Camera.WindowToNative((Point2)Input.Mouse.Position);
		var subtexture = Graphics.Atlas.Get(Spritesheet.Sprites, 1);
		Graphics.Batcher.Image(subtexture, position, Color.White);
	}
}
