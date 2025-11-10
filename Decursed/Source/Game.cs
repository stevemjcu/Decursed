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
	private readonly Dictionary<string, Subtexture> Subtextures;

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
			var name = Path.GetFileNameWithoutExtension(it);
			var file = new Aseprite(it);
			var frames = file.RenderAllFrames();
			var size = frames[0].Size / Config.TileSize;

			for (var i = 0; i < size.X; i++)
			{
				for (var j = 0; j < size.Y; j++)
				{
					var clip = new RectInt
					(
						new Point2(i, j) * Config.TileSize,
						Config.TileSize
					);

					for (var k = 0; k < frames.Length; k++)
					{
						packer.Add(string.Join('/', [name, i, j, k]), frames[k], clip);
					}
				}
			}
		}

		var output = packer.Pack();
		var texture = new Texture(device, output.Pages.Single());
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
		Level.Update();
	}

	protected override void Render()
	{
		Buffer.Clear(Color.Black);
		Window.Clear(Color.Black);

		Level.Render();

		var mousePosition = Camera.WindowToNative((Point2)Input.Mouse.Position);
		var subtexture = Subtextures["Player/0/0/0"];
		Batcher.Image(subtexture, mousePosition, Color.White);

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
