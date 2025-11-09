using Foster.Framework;
using System.Numerics;

namespace Decursed.Source;

/// <summary>
/// Manages the game loop.
/// </summary>
internal class Game : App
{
	private readonly Batcher Batch;
	private readonly Texture Texture;

	public Game() : base
	(
		Config.Title,
		Config.DisplayResolution.X,
		Config.DisplayResolution.Y
	)
	{
		Batch = new(GraphicsDevice);
		Texture = new(GraphicsDevice, new(128, 128, Color.Blue));
	}

	protected override void Startup()
	{
		// TODO:
		// Load and pack textures
	}

	protected override void Shutdown() { }

	protected override void Update() { }

	protected override void Render()
	{
		Window.Clear(Color.Black);

		Batch.PushMatrix
		(
			new Vector2(Window.WidthInPixels, Window.HeightInPixels) / 2,
			new Vector2(Texture.Width, Texture.Height) / 2,
			Vector2.One,
			(float)Time.Elapsed.TotalSeconds * 4.0f
		);

		Batch.Image(Texture, Vector2.Zero, Color.White);
		Batch.PopMatrix();

		Batch.Circle(new(Input.Mouse.Position, 8), 16, Color.White);

		Batch.Render(Window);
		Batch.Clear();
	}
}
