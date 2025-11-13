using Decursed.Source.General;
using Foster.Framework;
using System.Numerics;

namespace Decursed.Source;

/// <summary>
/// Manages the game loop.
/// </summary>
internal class Game : App
{
	private readonly Batcher Batcher;
	private readonly Target Buffer;
	private readonly Atlas Atlas = new();

	private readonly Camera Camera;
	private readonly Level.Level Level;

	public Game() : base
	(
		Config.Title,
		Config.WindowResolution.X,
		Config.WindowResolution.Y
	)
	{
		Batcher = new(GraphicsDevice);
		Buffer = new(GraphicsDevice, Config.NativeResolution.X, Config.NativeResolution.Y);

		foreach (var it in Directory.EnumerateFiles(Config.TexturePath)) Atlas.Add(it);
		Atlas.Pack(GraphicsDevice);

		Camera = new Camera()
		{
			NativeResolution = Config.NativeResolution,
			WindowResolution = Config.WindowResolution
		};

		Level = new Level.Level(Path.Combine(Config.LevelPath, "00"));
	}

	protected override void Startup() { }

	protected override void Shutdown() { }

	protected override void Update()
	{
		Level.Update();
	}

	protected override void Render()
	{
		Window.Clear(Color.Black);
		Buffer.Clear(Color.Black);

		Level.Render();

		var position = Camera.WindowToNative((Point2)Input.Mouse.Position);
		var subtexture = Atlas.Get(Spritesheet.Player.ToString(), new(0, 0));
		Batcher.Image(subtexture, position, Color.White);

		// Render to buffer
		Batcher.Render(Buffer);
		Batcher.Clear();

		Batcher.PushSampler(new(TextureFilter.Nearest, TextureWrap.Clamp));
		Batcher.PushMatrix(Vector2.Zero, new(Config.Scale), 0);
		Batcher.Image(Buffer, Color.White);
		Batcher.PopMatrix();
		Batcher.PopSampler();

		// Render to window
		Batcher.Render(Window);
		Batcher.Clear();
	}
}
