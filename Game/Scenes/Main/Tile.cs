using Nez;

namespace Game.Scenes.Main;

internal class Tile : Entity {
    public Entity Plant { get; private set; }
    public Entity Ground { get; private set; }
}
