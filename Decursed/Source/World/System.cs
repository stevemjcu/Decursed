using Foster.Framework;
using YetAnotherEcs;
using static Decursed.Components;

namespace Decursed;

internal abstract class System(World world)
{
	protected readonly World World = world;

	protected int Player => World.View(new Filter().Include<Receiver>())[0];

	protected int Instance => World.Get<ChildOf>(Player).Id;

	public abstract void Tick(Time time);
}
