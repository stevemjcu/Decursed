using Foster.Framework;

namespace Decursed;

internal interface IScene : IDisposable
{
	public void Update(Time delta);

	public void Render(Time delta);

	public void Overlay(Time delta);
}
