using Arch.Core;
using Decursed.Source.General;
using static Decursed.Source.Level.Components;

namespace Decursed.Source.Level;

/// <summary>
/// Manages the call stack and topmost instance.
/// </summary>
internal class Level : IScene, IDisposable
{
	private readonly Dictionary<int, Template> Templates = []; // template id : template
	private readonly Stack<Instance> Instances = []; // call stack
	private readonly World World = World.Create(); // entities

	private Instance Instance => Instances.Peek();

	public Level(string path)
	{
		foreach (var it in Directory.EnumerateFiles(path, "*.csv"))
		{
			var template = new Template(it);
			Templates.Add(template.Id, template);
		}

		// Enter root instance
		Enter(new Instance(Templates[0]));
	}

	public void Dispose() => World.Dispose();

	public void Update() { }

	public void Render()
	{
		var layout = Instance.Template.Layout;

		for (var x = 0; x < layout.GetLength(0); x++)
		{
			for (var y = 0; y < layout.GetLength(1); y++)
			{
				// TODO: Render tile
			}
		}

		var query = new QueryDescription().WithAll<Sprite, Body, Active>();
		World.Query(in query, (ref Sprite s, ref Body b) =>
		{
			// TODO: Render sprite (if active)
		});
	}

	private void Enter(Instance instance)
	{
		Instances.Push(new Instance(Templates[0]));
		// Add entities to world
		// Replace active tag
	}

	private void Exit()
	{
		var instance = Instances.Pop();
		// Remove entities from world
		// Replace active tag
	}
}