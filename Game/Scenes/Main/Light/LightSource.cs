using Game.Scenes.Main.Grid;
using Microsoft.Xna.Framework;
using Nez;

namespace Game.Scenes.Main.Light;

internal class LightSource : Component {
    public float LightIntensity;
    private GridContainer? grid;

    public LightSource(float lightIntensity) {
        LightIntensity = lightIntensity;
    }

    public override void OnAddedToEntity() {
        grid = Entity.Scene.GetSceneComponent<GridContainer>();
        AddLight();
    }

    public override void OnRemovedFromEntity() {
        RemoveLight();
    }

    private void AddLight() {
        for (var i = 0; i < grid!.Cols; i++) {
            for (var j = 0; j < grid!.Rows; j++) {
                var tile = grid![i, j];
                LightTile(tile!);
            }
        }
    }

    private void RemoveLight() {
        for (var i = 0; i < grid!.Cols; i++)
        {
            for (var j = 0; j < grid!.Rows; j++)
            {
                var tile = grid![i, j];
                UnlightTile(tile!);
            }
        }
    }

    private void LightTile(Tile tile) {
        foreach (var gridElement in tile) {
            var lightable = gridElement.GetComponent<Lightable>();
            if (lightable is not null) {
                lightable.AddLight(CalculateLightIntensityAt(tile.Position));
            }
        }
    }

    private void UnlightTile(Tile tile) {
        foreach (var gridElement in tile) {
            var lightable = gridElement.GetComponent<Lightable>();
            if (lightable is not null) {
                lightable.RemoveLight(CalculateLightIntensityAt(tile.Position));
            }
        }
    }

    private float CalculateLightIntensityAt(Vector2 otherPosition)
    {
        if (HasLightObstructionBetween(otherPosition, Entity.Position)) {
            return 0;
        }
        var distance = Vector2.Distance(Entity.Position, otherPosition);
        var intensity = Math.Clamp(LightIntensity / distance, 0, 1);
        return intensity < Constants.Threshold ? 0 : intensity;
    }

    private bool HasLightObstructionBetween(Vector2 from, Vector2 to) {
        var hit = Physics.Linecast(from, to, LightObstruction.ColliderLayer);
        return hit.Collider is not null;
    }
}
