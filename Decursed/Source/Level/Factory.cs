using Flecs.NET.Core;
using static Decursed.Source.Level.Components;

namespace Decursed.Source.Level;

internal class Factory
{
	private World World;
	private Entity Actor;

	public Factory(World world)
	{
		World = world;
		Actor = World.Prefab()
			.Add<Sprite>()
			.Add<Bounds>().Set<Bounds>(new(Config.UnitSize))
			.Add<Position>()
			.Add<Velocity>()
			.Add<Gravity>();
	}

	public Entity CreateActor(Archetype type)
	{
		var entity = World.Entity().IsA(Actor);

		switch (type)
		{
			case Archetype.Player:
				entity.Set<Sprite>(new(Archetype.Player));
				break;
		}

		return entity;
	}

	public Entity CreateTemplate(int id, string[,] layout) => default;

	public Entity CreateRoom(Entity template) => default;
}
