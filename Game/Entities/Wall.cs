using Game.Components.Light;
using Game.Tiles;

namespace Game.Entities;

internal class Wall : TileElement
{
    public override int TileIndex => Tile.WallIndex;

    public override void OnAddedToScene()
    {
        base.OnAddedToScene();
        AddComponent<LightObstruction>();
        RenderLayer = Constants.RenderingLayers.Wall;
    }
}
