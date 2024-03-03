using Game.Entities;
using Nez;

namespace Game.Components.PlayerComponents;

internal class PlayerController : Component, IUpdatable
{
    private Player? player;
    private PlayerControlsMap? controls;
    private CameraTarget? cameraTarget;

    public override void OnAddedToEntity()
    {
        player = (Player)Entity;
        controls = player.GetComponent<PlayerControlsMap>();
        cameraTarget = player.GetComponent<CameraTarget>();
    }

    public void Update()
    {
        player!.Force = controls!.Movement.Value;
        cameraTarget!.TargetPosition = player!.Position;
    }
}
