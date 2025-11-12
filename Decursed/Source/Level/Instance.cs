namespace Decursed.Source.Level;

/// <summary>
/// An instance of a room.
/// </summary>
internal class Instance(Template template)
{
	public Guid Id { get; private set; } = Guid.NewGuid();

	public Template Template { get; private set; } = template;
}
