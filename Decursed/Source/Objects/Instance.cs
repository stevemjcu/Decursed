namespace Decursed.Source.Objects;

internal class Instance(Template template)
{
	public readonly Template Template = template;
	public readonly List<Entity> Entities = [.. template.Entities];
}
