using Game.Components;
using Game.Tiles;

namespace Game.Entities;

internal class Obstacle : TileElement
{
    public override int TileIndex => Tile.EntityIndex;

    public override void OnAddedToScene()
    {
        base.OnAddedToScene();
        RenderLayer = Constants.RenderingLayers.WallsAndEntities;
        var width = Animator!.Width;
        var height = Animator!.Height;
        AddComponent(new ColliderShape(width, height * 0.3f, true));
    }
}
