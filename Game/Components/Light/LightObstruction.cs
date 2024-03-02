using Nez;

namespace Game.Components.Light;

internal class LightObstruction : BoxCollider
{
    public static readonly int ColliderLayer = 1;

    public LightObstruction()
    {
        PhysicsLayer = ColliderLayer;
    }
}
