using Microsoft.Xna.Framework;
using Nez;

namespace Game.Components;

internal class CameraTarget : Component, IUpdatable
{
    public Vector2 TargetPosition;

    [Inspectable]
    private float mass;
    private Vector2 acceleration;
    private Vector2 velocity;

    public CameraTarget()
    {
        mass = Constants.Camera.Mass;
    }

    public override void OnAddedToEntity()
    {
        TargetPosition = Entity.Position;
    }

    public void Update()
    {
        var currentPosition = Entity.Scene.Camera.Position;
        acceleration = (TargetPosition - currentPosition) / mass;
        velocity = acceleration * Time.DeltaTime;
        Entity.Scene.Camera.Position = currentPosition + velocity * Time.DeltaTime;
    }
}
