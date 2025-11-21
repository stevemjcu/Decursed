using Decursed.Source.Utility;
using Decursed.Source.Data;
using Decursed.Source.Objects;

namespace Decursed.Source.Scenes;

/// <summary>
/// Manages the call stack and topmost room.
/// </summary>
internal class Level : IScene
{
	private readonly Dictionary<int, Template> Templates = [];
	private readonly Stack<Instance> Instances = [];

	public Level(string path)
	{
		foreach (var it in Directory.EnumerateFiles(path))
		{
			var id = int.Parse(Path.GetFileNameWithoutExtension(it));
			var content = Parser.ReadCsv(it, Config.LevelSize);

			Templates[id] = new Template(content);
		}

		Instances.Push(new(Templates[0]));
	}

	public void Update() { }

	public void Render()
	{
		// TODO: Render room parented to player
	}
}