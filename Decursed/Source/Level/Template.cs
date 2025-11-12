namespace Decursed.Source.Level;

/// <summary>
/// A template of a room.
/// </summary>
internal class Template
{
	public int Id { get; private set; }

	public bool[,] Layout { get; private set; }

	public Template(string path)
	{
		Id = int.Parse(Path.GetFileNameWithoutExtension(path));
		// TODO: Load layout
	}
}
