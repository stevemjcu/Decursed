using Foster.Framework;

namespace Decursed.Source;

internal class Atlas
{
	private readonly Packer Packer = new();
	private readonly Dictionary<string, Subtexture> Subtextures = [];

	public void Add(string path)
	{
		var name = Path.GetFileNameWithoutExtension(path);
		var frames = new Aseprite(path).RenderAllFrames();
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
					Packer.Add(string.Join('/', [name, i, j, k]), frames[k], clip);
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

	public Subtexture Get(string page, Point2 position, int frame = 0) =>
		Subtextures[string.Join('/', [page, position.X, position.Y, frame])];
}
