using Game.Scenes.Main.Tiles;
using Microsoft.Xna.Framework;
using Nez;

namespace Game.Scenes.Main;

internal class MainScene : BaseScene {
    public Grid Grid { get; private set; }

    public MainScene() {
        Grid = new Grid(128, 128, new Vector2(16, 16));
    }

    public override void OnStart() {
        Camera.SetZoom(1);
        Camera.Position = new Vector2(128 * 16 / 2, 128 * 16 / 2);

        Grid = AddSceneComponent(new Grid(128, 128, new Vector2(16, 16)));
        var worldBoundaries = new RectangleF(0, 0, 128 * 16, 128 * 16);
        AddSceneComponent(new MouseControl(worldBoundaries));

        RunProceduralGeneration();
    }

    public override void Begin() {
        base.Begin();
    }

    private void RunProceduralGeneration() {
        for (var i = 0; i < Grid.Rows; i++) {
            for (var j = 0; j < Grid.Cols; j++) {
                if ((j % 2 ^ i % 2) == 0)
                Grid[i, j] = new Soil();
            }
        }
    }
}
