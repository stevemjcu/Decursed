using Foster.Framework;

namespace Decursed;

internal class Graphics : IDisposable
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
