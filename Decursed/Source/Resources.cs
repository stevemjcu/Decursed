using Foster.Framework;

namespace Decursed;

/// <summary>
/// Contains resources created at game start-up.
/// </summary>
internal class Resources : IDisposable
{
	public required Batcher Batcher;
	public required Target Buffer;
	public required Camera Camera;
	public required Atlas Atlas;

	public void Dispose()
	{
		Batcher.Dispose();
		Buffer.Dispose();
		Atlas.Dispose();
	}
}
