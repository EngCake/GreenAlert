using Game.Tiles;

namespace Game.Entities;

internal class Chest : TileElement
{
    public override int TileIndex => Tile.EntityIndex;

    public override void OnAddedToScene()
    {
        base.OnAddedToScene();
        RenderLayer = Constants.RenderingLayers.Entities;
    }
}
