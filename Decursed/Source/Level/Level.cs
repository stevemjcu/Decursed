using Arch.Core;
using Decursed.Source.General;

namespace Decursed.Source.Level;

/// <summary>
/// Manages game logic and meta state.
/// </summary>
internal class Level : IScene
{
	private readonly Dictionary<Entity, int> Globals; // entity : template id
	private readonly List<Template> Templates; // template id : template
	private readonly Stack<Instance> Instances; // call stack

	public Level(string folderPath)
	{
		// TODO: Load templates/globals
		// TODO: Instantiate first template
	}

	public void Update() { }

	public void Render() { }
}