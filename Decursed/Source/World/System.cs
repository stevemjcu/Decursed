using Foster.Framework;
using YetAnotherEcs;
using YetAnotherEcs.General;
using static Decursed.Components;

namespace Decursed;

internal abstract class System(World world)
{
	protected readonly World World = world;

	protected int Player => World.View(new Filter().Include<Receiver>())[0];

	protected int Room => World.Get<ChildOf>(Player).Id;

	protected int[,] Tilemap => World.Get<Tilemap>(Room).Value;

	protected IIndexableSet<int> Local => World.View<ChildOf>(new(Room));

	public abstract void Update(Time time);
}
