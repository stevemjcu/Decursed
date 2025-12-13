using Foster.Framework;
using YetAnotherEcs;
using YetAnotherEcs.General;
using static Decursed.Components;

namespace Decursed;

internal abstract class System(World world)
{
	protected readonly World World = world;

	protected int Player => World.View(new Filter().Include<Receiver>())[0];

	protected int Instance => World.Get<ChildOf>(Player).Id;

	protected int[,] Tilemap => World.Get<Tilemap>(Instance).Value;

	protected IIndexableSet<int> LocalView => World.View(new ChildOf(Instance));

	public abstract void Update(Time time);
}
