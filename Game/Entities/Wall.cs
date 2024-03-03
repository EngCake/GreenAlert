using Game.Components;
using Game.Tiles;

namespace Game.Entities;

internal class Wall : TileElement
{
    public override int TileIndex => Tile.WallIndex;

    public override void OnAddedToScene()
    {
        base.OnAddedToScene();
        AddComponent(new ColliderShape(1, 0.3f, true));
        RenderLayer = Constants.RenderingLayers.WallsAndEntities;
    }
}
