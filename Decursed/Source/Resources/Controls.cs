using Foster.Framework;

namespace Decursed;

internal class Controls(Foster.Framework.Input Input) {
	public readonly VirtualStick Move =
		new(Input, "Move", new StickBindingSet().AddWasd());

	public readonly VirtualAction Jump =
		new(Input, "Jump", new ActionBindingSet().Add(Keys.K));

	public readonly VirtualAction Interact =
		new(Input, "Interact", new ActionBindingSet().Add(Keys.J));
}
