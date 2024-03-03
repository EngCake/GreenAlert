using Nez;

namespace Game.Components;

internal class ColliderShape : BoxCollider
{
    public ColliderShape(float width, float height, bool blocksLight) : base(new RectangleF(-width / 2, height / 2, width, height))
    {
        PhysicsLayer = blocksLight ? Constants.PhysicsLayers.LightBlockingCollider : Constants.PhysicsLayers.NonLightBlockingCollider;
    }
}
