using Game.Components;
using Game.Components.Light;
using Game.Tiles;
using Microsoft.Xna.Framework;

namespace Game.Entities;

internal class CrystalTree : TileElement, IUprightEntity
{
    public int LightRadius { get; private set; }

    public override int TileIndex => Tile.EntityIndex;

    public Vector2 Center => Position;

    public int OriginalRenderLayer => Constants.RenderingLayers.WallsAndEntities;

    public CrystalTree(int lightRadius)
    {
        LightRadius = lightRadius;
    }

    public override void OnAddedToScene()
    {
        base.OnAddedToScene();
        RenderLayer = OriginalRenderLayer;
        AddComponent(new LightSource(LightRadius));
        var width = Animator!.Width;
        var height = Animator!.Height;
        AddComponent(new ColliderShape(width * 0.35f, height * 0.3f, false));
    }
}
