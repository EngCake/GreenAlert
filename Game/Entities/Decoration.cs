using Game.Tiles;
using Microsoft.Xna.Framework;

namespace Game.Entities;

internal class Decoration : TileElement, IUprightEntity
{
    public override int TileIndex => Tile.DecorationIndex;

    public Vector2 Center => Position + new Vector2(0, -Animator!.Height / 5);

    public int OriginalRenderLayer => Constants.RenderingLayers.Decoration;

    public override void OnAddedToScene()
    {
        base.OnAddedToScene();
        RenderLayer = OriginalRenderLayer;
    }
}
