using Arch.Core;

namespace Decursed.Source.Room;

/// <summary>
/// Manages game logic and meta state.
/// </summary>
internal class Scene
{
	private readonly Dictionary<Entity, int> Globals; // entity : template id
	private readonly Dictionary<int, Template> Templates; // template id : template
	private readonly Stack<Instance> Instances; // call stack

	public Scene(string folderPath)
	{
		// Load templates/globals
		// Instantiate first template
	}

	public void Update() { }

	public void Render() { }
}