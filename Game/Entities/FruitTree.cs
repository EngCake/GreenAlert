using Game.Components;
using Game.Tiles;
using Microsoft.Xna.Framework;

namespace Game.Entities;

internal class FruitTree : TileElement, IUprightEntity
{
    public override int TileIndex => Tile.EntityIndex;

    public Vector2 Center => Position;

    public int OriginalRenderLayer => Constants.RenderingLayers.WallsAndEntities;

    public override void OnAddedToScene()
    {
        base.OnAddedToScene();
        RenderLayer = OriginalRenderLayer;
        var width = Animator!.Width;
        var height = Animator!.Height;
        AddComponent(new ColliderShape(width * 0.35f, height * 0.3f, false));
    }
}
