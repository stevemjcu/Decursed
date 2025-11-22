using Foster.Framework;

namespace Decursed.Library.Source;

public class Atlas
{
	private readonly Packer Packer = new();
	private readonly Dictionary<string, Subtexture> Subtextures = [];

	public void Add(string path, Point2 resolution)
	{
		var name = Path.GetFileNameWithoutExtension(path);
		var frames = new Aseprite(path).RenderAllFrames();
		var size = frames[0].Size / resolution;

		for (var i = 0; i < size.X; i++)
		{
			for (var j = 0; j < size.Y; j++)
			{
				var position = new Point2(i, j);
				var clip = new RectInt
				(
					position * resolution,
					resolution
				);

				for (var k = 0; k < frames.Length; k++)
				{
					Packer.Add(CreateIndex(name, position, k), frames[k], clip);
				}
			}
		}
	}

	public void Pack(GraphicsDevice device)
	{
		var output = Packer.Pack();
		var texture = new Texture(device, output.Pages.Single());

		foreach (var it in output.Entries)
		{
			Subtextures.Add(it.Name, new(texture, it.Source, it.Frame));
		}
	}

	public Subtexture Get(string name, Point2 position, int frame = 0) =>
		Subtextures[CreateIndex(name, position, frame)];

	private static string CreateIndex(string name, Point2 position, int frame) =>
		string.Join('/', [name, position.X, position.Y, frame]);
}
