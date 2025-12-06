namespace Decursed;

/// <summary>
/// Manages the game loop within a scope.
/// </summary>
internal interface IScene : IDisposable
{
	public void Update();

	public void Render();
}
