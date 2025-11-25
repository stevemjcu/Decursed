using Foster.Framework;
using System.Numerics;

namespace Decursed;

/// <summary>
/// Manages the game loop.
/// </summary>
internal class Game : App
{
	internal readonly Resources Resources;
	internal readonly Stack<IScene> Scenes = [];

	public Game() : base
	(
		Config.Title,
		Config.WindowResolution.X,
		Config.WindowResolution.Y
	)
	{
		var atlas = new Atlas(GraphicsDevice);
		foreach (var it in Directory.EnumerateFiles(Config.TexturePath)) atlas.Add(it);
		atlas.Pack();

		Resources = new()
		{
			Batcher = new(GraphicsDevice),
			Buffer = new(GraphicsDevice, Config.NativeResolution.X, Config.NativeResolution.Y),
			Camera = new(Config.WindowResolution, Config.NativeResolution, Config.LevelSize),
			Atlas = atlas
		};

		Scenes.Push(new Level(Path.Combine(Config.LevelPath, "00"), Resources));
	}

	protected override void Startup() => Window.SetMouseVisible(false);

	protected override void Shutdown() { }

	protected override void Update() => Scenes.Peek().Update();

	protected override void Render()
	{
		Window.Clear(Color.Black);
		Resources.Buffer.Clear(Color.Black);

		Scenes.Peek().Render();
		DrawCursor();

		// Render to buffer
		Resources.Batcher.Render(Resources.Buffer);
		Resources.Batcher.Clear();

		Resources.Batcher.PushSampler(new(TextureFilter.Nearest, TextureWrap.Clamp));
		Resources.Batcher.PushMatrix(Vector2.Zero, new(Config.WindowScale), 0);
		Resources.Batcher.Image(Resources.Buffer, Color.White);
		Resources.Batcher.PopMatrix();
		Resources.Batcher.PopSampler();

		// Render to window
		Resources.Batcher.Render(Window);
		Resources.Batcher.Clear();
	}

	private void DrawCursor()
	{
		var position = Resources.Camera.WindowToNative((Point2)Input.Mouse.Position);
		var subtexture = Resources.Atlas.Get(Config.Spritesheet.Actors, 1);
		Resources.Batcher.Image(subtexture, position, Color.White);
	}
}
