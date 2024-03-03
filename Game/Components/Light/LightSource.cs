using Game.Tiles;
using Microsoft.Xna.Framework;
using Nez;

namespace Game.Components.Light;

internal class LightSource : Component
{
    public float LightRadius { get; private set; }

    private TilesContainer? grid;

    public LightSource(float lightRadius)
    {
        LightRadius = lightRadius;
    }

    public override void OnAddedToEntity()
    {
        grid = Entity.Scene.GetSceneComponent<TilesContainer>();
    }

    public void Light()
    {
        ArgumentNullException.ThrowIfNull(grid);

        //int top = (int) Math.Max(0, (Entity.Position.Y - LightRadius * 2) / grid.Height);
        //int bottom = (int)Math.Min(grid.Rows - 1, (Entity.Position.Y + LightRadius * 2) / grid.Height);
        //int left = (int)Math.Max(0, (Entity.Position.X - LightRadius * 2) / grid.Width);
        //int right = (int)Math.Min(grid.Cols - 1, (Entity.Position.X + LightRadius * 2) / grid.Width);

        int top = 0;
        int bottom = grid.Rows - 1;
        int left = 0;
        int right = grid.Cols - 1;

        for (int i = top; i <= bottom; i++)
        {
            for (int j = left; j <= right; j++)
            {
                var tile = grid![i, j];
                if (CheckIfLightPassesTo(tile.Position, Entity.Position) && CheckLightAt(tile.Position))
                {
                    tile.LightsCount++;
                }
            }
        }
    }

    private bool CheckLightAt(Vector2 otherPosition)
    {
        var distance = Vector2.Distance(Entity.Position, otherPosition);
        return distance <= LightRadius;
    }

    private bool CheckIfLightPassesTo(Vector2 entityPosition, Vector2 lightPosition)
    {
        return Physics.Linecast(entityPosition, lightPosition, Constants.PhysicsLayers.LightBlockingCollider).Collider is null;
    }
}
