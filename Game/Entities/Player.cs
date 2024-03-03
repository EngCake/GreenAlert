using Game.Components;
using Game.Components.PlayerComponents;
using Game.Tiles;
using Microsoft.Xna.Framework;

namespace Game.Entities;

internal class Player : TileElement, IMovable
{
    public override int TileIndex => Tile.EntityIndex;

    public Vector2 Force { get; set; } = Vector2.Zero;
    public Vector2 Direction { get; set; } = Vector2.Zero;
    public Vector2 Velocity { get; set; } = Vector2.Zero;
    public Vector2 Acceleration { get; set; } = Vector2.Zero;
    
    public Player()
    {
        Name = "player";
    }

    public override void OnAddedToScene()
    {
        base.OnAddedToScene();
        RenderLayer = Constants.RenderingLayers.Player;
        var width = Animator!.Width;
        var height = Animator!.Height;
        AddComponent(new ColliderShape(width * 0.5f, height * 0.3f, false));
        AddComponent<CameraTarget>();
        AddComponent(new Mover(false));
        AddComponent<PlayerControlsMap>();
        AddComponent<PlayerAnimationController>();
        AddComponent<PlayerController>();
    }
}
