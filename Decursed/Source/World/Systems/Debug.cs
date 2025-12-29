using Foster.Framework;
using YetAnotherEcs;
using static Decursed.Components;

namespace Decursed;

internal class Debug(World World, Graphics Graphics) : System(World) {
	public override void Update(Time time) {
		if (!Config.DebugMode) {
			return;
		}

		foreach (var it in World.View(new Filter().Include<Position, Hitbox, Focused>())) {
			var position = it.Get<Position>().Value;
			var hitbox = it.Get<Hitbox>().Value;

			var screenPosition = Graphics.Camera.WorldToWindow(position);
			var screenScale = Graphics.Camera.WorldToWindow(Config.UnitSize);

			hitbox = hitbox.Scale(screenScale).Translate(screenPosition);
			Graphics.Batcher.RectLine(hitbox, 1, Color.Green);
		}
	}
}
