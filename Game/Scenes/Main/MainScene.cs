using Game.Scenes.Main.Grid;
using Game.Scenes.Main.Ground;
using Microsoft.Xna.Framework;
using Nez;
using Random = Nez.Random;

namespace Game.Scenes.Main;

internal class MainScene : BaseScene {
    public GridContainer? Grid { get; private set; }

    public MainScene() : base() {}

    public override void OnStart() {
        Camera.SetZoom(-0.5f);
        Camera.Position = new Vector2(Constants.Cols * Constants.CellWidth / 2, Constants.Rows * Constants.CellHeight / 2);

        Grid = AddSceneComponent<GridContainer>();
        var worldBoundaries = new RectangleF(-Constants.CellWidth / 2, -Constants.CellHeight / 2, Constants.Cols * Constants.CellWidth, Constants.Rows * Constants.CellHeight);
        AddSceneComponent(new MouseControl(worldBoundaries));

        RunProceduralGeneration();
    }

    public override void Begin() {
        base.Begin();
    }

    private void RunProceduralGeneration() {
        for (var i = 0; i < Grid!.Rows; i++) {
            for (var j = 0; j < Grid.Cols; j++) {
                if (Random.Chance(10)) {
                    Grid[i, j].Ground = new Boulder();
                }
                else {
                    Grid[i, j].Ground = new Soil();
                }
            }
        }
    }
}
