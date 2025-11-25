using Foster.Framework;

namespace Decursed;

internal class Atlas
{
	private readonly Packer Packer = new();
	private readonly Dictionary<string, Subtexture> Subtextures = [];

	public void Add(string path)
	{
		var name = Path.GetFileNameWithoutExtension(path);
		var frames = new Aseprite(path).RenderAllFrames();
		var size = frames[0].Size / Config.TileResolution;

		for (var x = 0; x < size.X; x++)
		{
			for (var y = 0; y < size.Y; y++)
			{
				var index = size.X * y + x;
				var position = new Point2(x, y);
				var clip = new RectInt
				(
					position * Config.TileResolution,
					Config.TileResolution
				);

				for (var z = 0; z < frames.Length; z++)
				{
					Packer.Add(CreateIndex(name, index, z), frames[z], clip);
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

	public Subtexture Get(string name, int index, int frame = 0) =>
		Subtextures[CreateIndex(name, index, frame)];

	public Subtexture Get(IFormattable name, int index, int frame = 0) =>
		Subtextures[CreateIndex(name.ToString()!, index, frame)];

	private static string CreateIndex(string name, int index, int frame) =>
		string.Join('/', [name, index, frame]);
}
