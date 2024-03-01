using Nez;
using Nez.PhysicsShapes;

namespace Game.Scenes.Main.Light;

internal class LightObstruction : BoxCollider
{
    public static readonly int ColliderLayer = 1;

    public LightObstruction()
    {
        PhysicsLayer = ColliderLayer;
    }
}
