using Game.Tiles;

namespace Game.Entities;

internal class Ground : TileElement
{
    public bool IsPlantable;

    public override int TileIndex => Tile.GroundIndex;

    public override void OnAddedToScene()
    {
        base.OnAddedToScene();
        RenderLayer = Constants.RenderingLayers.Ground;
    }
}
