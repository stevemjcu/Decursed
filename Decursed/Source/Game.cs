using Decursed.Source.General;
using Decursed.Source.Room;
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
	private readonly TextureSampler Sampler = new(TextureFilter.Nearest, TextureWrap.Clamp);

	private Texture Atlas;
	private Dictionary<string, Subtexture> Subtextures;

	private readonly Camera Camera;
	private readonly Level Level;

	public Game() : base
	(
		Config.Title,
		Config.WindowResolution.X,
		Config.WindowResolution.Y
	)
	{
		Batcher = new(GraphicsDevice);
		Buffer = new(GraphicsDevice, Config.NativeResolution.X, Config.NativeResolution.Y);

		Subtextures = PackTextures(GraphicsDevice, Config.TexturePath);
		Atlas = Subtextures.First().Value.Texture!;

		Camera = new Camera()
		{
			NativeResolution = Config.NativeResolution,
			WindowResolution = Config.WindowResolution
		};

		Level = new Level(Path.Combine(Config.LevelPath, "00"));
	}

	private static Dictionary<string, Subtexture> PackTextures(GraphicsDevice device, string path)
	{
		var packer = new Packer();

		foreach (var it in Directory.EnumerateFiles(path))
		{
			packer.Add
			(
				Path.GetFileNameWithoutExtension(it),
				new Aseprite(it).RenderFrame(0)
			);
		}

		var output = packer.Pack();
		var texture = new Texture(device, output.Pages[0]);
		var subtextures = new Dictionary<string, Subtexture>();

		foreach (var it in output.Entries)
		{
			subtextures.Add(it.Name, new(texture, it.Source, it.Frame));
		}

		return subtextures;
	}

	protected override void Startup() { }

	protected override void Shutdown() { }

	protected override void Update()
	{
		//Scene.Update();
	}

	protected override void Render()
	{
		Buffer.Clear(Color.Black);
		Window.Clear(Color.Black);

		//Scene.Render();

		var position = Camera.WindowToNative((Point2)Input.Mouse.Position);
		Batcher.Circle(new(position, 2), 16, Color.White);

		// Render to buffer
		Batcher.Render(Buffer);
		Batcher.Clear();

		Batcher.PushSampler(Sampler);
		Batcher.Image(Buffer, Vector2.Zero, Vector2.Zero, new(Config.Scale), 0, Color.White);
		Batcher.PopSampler();

		// Render to window
		Batcher.Render(Window);
		Batcher.Clear();
	}
}
