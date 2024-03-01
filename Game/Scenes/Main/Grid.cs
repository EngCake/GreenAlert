using Microsoft.Xna.Framework;
using Nez;

namespace Game.Scenes.Main;

internal class Grid : SceneComponent {
    public int Cols { get; }
    public int Rows { get; }
    private readonly Vector2 gridSize;
    private readonly Entity[,] entities;

    public Grid(int colsCount, int rowsCount, Vector2 gridSize) {
        entities = new Entity[colsCount, rowsCount];
        this.Cols = colsCount;
        this.Rows = rowsCount;
        this.gridSize = gridSize;
    }

    public Entity this[int x, int y] {
        get {
            if (x >= Cols || y >= Rows) {
                throw new ArgumentException($"Grid[{x}, {y}] isn't valid for a grid of size ({Cols}, {Rows})");
            }
            return entities[x, y];
        }
        set {
            if (x >= Cols || y >= Rows) {
                throw new ArgumentException($"Grid[{x}, {y}] isn't valid for a grid of size ({Cols}, {Rows})");
            }
#if DEBUG
            if (Scene.FindEntity(value.Name) != null) {
                throw new Exception($"An entity with the name {value.Name} is already added");
            }
#endif
            value.Position = GetWorldCoordinates(x, y);
            Scene.AddEntity(value);
            entities[x, y] = value;
        }
    }

    public Vector2 GetWorldCoordinates(int x, int y) {
        return gridSize * new Vector2(x, y);
    }
}
