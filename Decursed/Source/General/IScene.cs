using Foster.Framework;

namespace Decursed;

internal interface IScene : IDisposable
{
	public void Update(Time delta);

	public void RenderToBuffer(Time delta);

	public void RenderToScreen(Time delta);

	void IDisposable.Dispose() { }
}
