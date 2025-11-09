namespace Decursed.Source;

/// <summary>
/// The entry point of the application.
/// </summary>
internal static class Program
{
	public static void Main(string[] _)
	{
		using var game = new Game();
		game.Run();
	}
}
