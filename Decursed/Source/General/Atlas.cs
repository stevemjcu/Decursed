using Foster.Framework;

namespace Decursed;

internal class Atlas(GraphicsDevice device) : IDisposable {
	private readonly GraphicsDevice Device = device;
	private readonly Packer Packer = new();

	private Texture? Texture;
	private readonly Dictionary<string, Subtexture> Subtextures = [];

	public void Dispose() {
		Texture?.Dispose();
	}

	public void Add(string path) {
		var name = Path.GetFileNameWithoutExtension(path);
		var frames = new Aseprite(path).RenderAllFrames();
		var size = frames[0].Size / Config.TileResolution;

		for (var x = 0; x < size.X; x++) {
			for (var y = 0; y < size.Y; y++) {
				for (var z = 0; z < frames.Length; z++) {
					var clip = new RectInt
					(
						new Point2(x, y) * Config.TileResolution,
						Config.TileResolution
					);

					Packer.Add(CreateIndex(name, size.X * y + x, z), frames[z], clip);
				}
			}
		}
	}

	public void Pack() {
		var output = Packer.Pack();
		Texture = new Texture(Device, output.Pages.Single());

		foreach (var it in output.Entries) {
			Subtextures.Add(it.Name, new(Texture, it.Source, it.Frame));
		}
	}

	public Subtexture Get(IFormattable name, int index, int frame = 0) {
		return Get(name.ToString()!, index, frame);
	}

	public Subtexture Get(string name, int index, int frame = 0) {
		return Subtextures[CreateIndex(name, index, frame)];
	}

	private static string CreateIndex(string name, int index, int frame) {
		return string.Join('/', [name, index, frame]);
	}
}
