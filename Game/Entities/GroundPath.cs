using Game.Tiles;

namespace Game.Entities;

internal class GroundPath : TileElement
{
    public override int TileIndex => Tile.PathIndex;

    public override void OnAddedToScene()
    {
        base.OnAddedToScene();
        RenderLayer = Constants.RenderingLayers.Path;
    }
}
