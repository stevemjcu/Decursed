namespace Decursed;

internal interface IScene : IDisposable
{
	public void Update();

	public void Render();
}
