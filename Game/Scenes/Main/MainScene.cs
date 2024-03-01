using Game.Scenes.Main.Tiles;
using Microsoft.Xna.Framework;
using Nez;

namespace Game.Scenes.Main;

internal class MainScene : BaseScene {
    public Grid Grid { get; private set; }

    public MainScene() : base() {
        Grid = new Grid(128, 128, new Vector2(16, 16));
    }

    public override void OnStart() {
        Camera.SetZoom(0.5f);
        Camera.Position = new Vector2(Constants.Cols * Constants.CellWidth / 2, Constants.Rows * Constants.CellHeight / 2);

        Grid = AddSceneComponent(new Grid(Constants.Cols, Constants.Rows, new Vector2(Constants.CellWidth, Constants.CellHeight)));
        var worldBoundaries = new RectangleF(-Constants.CellWidth / 2, -Constants.CellHeight / 2, Constants.Cols * Constants.CellWidth, Constants.Rows * Constants.CellHeight);
        AddSceneComponent(new MouseControl(worldBoundaries));

        RunProceduralGeneration();
    }

    public override void Begin() {
        base.Begin();
    }

    private void RunProceduralGeneration() {
        for (var i = 0; i < Grid.Rows; i++) {
            for (var j = 0; j < Grid.Cols; j++) {
                if ((j % 2 ^ i % 2) == 0) {
                    var type = Nez.Random.Choose(SoilType.Brown, SoilType.Orange, SoilType.Blue);
                    Grid[i, j] = new Soil(type);
                }
                else
                    Grid[i, j] = new Water();
            }
        }
    }
}
