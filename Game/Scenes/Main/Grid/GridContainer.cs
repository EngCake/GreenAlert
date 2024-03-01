using Microsoft.Xna.Framework;
using Nez;

namespace Game.Scenes.Main.Grid;

internal class GridContainer : SceneComponent {
    public int Cols { get; }
    public int Rows { get; }

    private readonly Vector2 gridSize;
    private readonly Tile[,] tiles;

    public GridContainer() {
        tiles = new Tile[Constants.Cols, Constants.Rows];
        Cols = Constants.Cols;
        Rows = Constants.Rows;
        gridSize = new Vector2(Constants.CellWidth, Constants.CellHeight);
    }

    public Tile this[int x, int y] {
        get {
            if (x >= Cols || y >= Rows) {
                throw new ArgumentException($"Grid[{x}, {y}] isn't valid for a grid of size ({Cols}, {Rows})");
            }
            if (tiles[x, y] is null) {
                this[x, y] = new();
            }
            return tiles[x, y];
        }

        private set {
            value.Position = GetWorldCoordinates(x, y);
            tiles[x, y] = Scene.AddEntity(value);
        }
    }

    public Vector2 GetWorldCoordinates(int x, int y) {
        return gridSize * new Vector2(x, y);
    }
}
