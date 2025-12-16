using Foster.Framework;
using System.Numerics;

namespace Decursed;

internal class Game : App, IDisposable
{
	internal readonly Graphics Graphics;
	internal readonly Controls Controls;

	internal readonly Stack<IScene> Scenes = [];

	public Game() : base(
		Config.Title,
		Config.WindowResolution.X,
		Config.WindowResolution.Y)
	{
		var atlas = new Atlas(GraphicsDevice);

		foreach (var it in Directory.EnumerateFiles(Config.TexturePath))
		{
			atlas.Add(it);
		}

		atlas.Pack();

		Graphics = new()
		{
			Batcher = new(GraphicsDevice),
			Buffer = new(GraphicsDevice, Config.NativeResolution.X, Config.NativeResolution.Y),
			Camera = new(Config.WindowResolution, Config.NativeResolution, Config.LevelSize),
			Atlas = atlas
		};

		Controls = new(Input);
		Scenes.Push(new Level(Path.Combine(Config.LevelPath, "00"), this));
	}

	public new void Dispose()
	{
		Graphics.Dispose();

		foreach (var it in Scenes)
		{
			it.Dispose();
		}

		base.Dispose();
	}

	protected override void Startup()
	{
		Window.SetMouseVisible(false);
	}

	protected override void Shutdown() { }

	protected override void Update()
	{
		Scenes.Peek().Update(Time);
	}

	protected override void Render()
	{
		Window.Clear(Color.Black);
		Graphics.Buffer.Clear(Color.Black);

		// Render scene to buffer

		Scenes.Peek().RenderToBuffer(Time);
		DrawCursor();

		Graphics.Batcher.Render(Graphics.Buffer);
		Graphics.Batcher.Clear();

		// Render buffer to window

		Graphics.Batcher.PushSampler(new(TextureFilter.Nearest, TextureWrap.Clamp));
		Graphics.Batcher.PushMatrix(Vector2.Zero, new(Config.WindowScale), 0);
		Graphics.Batcher.Image(Graphics.Buffer, Color.White);
		Graphics.Batcher.PopMatrix();
		Graphics.Batcher.PopSampler();

		Scenes.Peek().RenderToScreen(Time);

		Graphics.Batcher.Render(Window);
		Graphics.Batcher.Clear();
	}

	private void DrawCursor()
	{
		var position = Graphics.Camera.WindowToNative((Point2)Input.Mouse.Position);
		var subtexture = Graphics.Atlas.Get(Config.Spritesheet.Actors, (int)Config.Actors.Cursor);
		Graphics.Batcher.Image(subtexture, position, Color.White);
	}
}
