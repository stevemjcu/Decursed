using Foster.Framework;
using YetAnotherEcs;
using static Decursed.Components;

namespace Decursed;

internal abstract class System(World world) {
	protected readonly World World = world;

	protected Entity Player => World.View(new Filter().Include<Receiver>())[0];

	protected Entity Room => Player.Get<ChildOf>().Value;

	protected int[,] Tilemap => Room.Get<Tilemap>().Value;

	public abstract void Update(Time time);
}
