using Arch.Core;

namespace Decursed;

/// <summary>
/// Manages the call stack and other meta state.
/// </summary>
internal class Scene
{
	private Stack<Instance> CallStack;
	private Dictionary<uint, Template> TemplateMap; // id : template
	private Dictionary<Entity, uint> GlobalMap; // entity : template

	public Scene(string path)
	{
		// Load templates/globals
		// Instantiate first template
	}
}