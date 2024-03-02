using Game.Tiles;

namespace Game.Entities;

internal class Decoration : TileElement
{
    public override int TileIndex => Tile.DecorationIndex;

    public override void OnAddedToScene()
    {
        base.OnAddedToScene();
        RenderLayer = Constants.RenderingLayers.Decoration;
    }
}
