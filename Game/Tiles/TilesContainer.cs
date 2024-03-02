using Microsoft.Xna.Framework;
using Nez;

namespace Game.Tiles;

internal class TilesContainer : SceneComponent
{
    /// <summary>
    /// Number of columns in the Grid
    /// </summary>
    public int Cols { get; private set; }

    /// <summary>
    /// Number of rows in the Grid
    /// </summary>
    public int Rows { get; private set; }

    /// <summary>
    /// Width of a single Grid cell
    /// </summary>
    public int Width { get; private set; }

    /// <summary>
    /// Height of a single Grid cell
    /// </summary>
    public int Height { get; private set; }

    public RectangleF Bounds => new RectangleF(-Width / 2.0f, -Height / 2.0f, Width * Cols, Height * Rows);

    private readonly Tile[,] tiles;

    public TilesContainer(int rows, int cols, int width, int height)
    {
        tiles = new Tile[cols, rows];
        Cols = cols;
        Rows = rows;
        Width = width;
        Height = height;
    }

    public Tile this[int x, int y]
    {
        get
        {
            if (x >= Cols || y >= Rows)
            {
                throw new ArgumentException($"Grid[{x}, {y}] isn't valid for a grid of size ({Cols}, {Rows})");
            }
            if (tiles[x, y] is null)
            {
                this[x, y] = new();
            }
            return tiles[x, y];
        }

        private set
        {
            value.Position = GetWorldCoordinates(x, y);
            tiles[x, y] = Scene.AddEntity(value);
        }
    }

    public Tile this[Vector2 position]
    {
        get
        {
            var (x, y) = GetIndex(position);
            return this[x, y];
        }
    }

    public Vector2 GetWorldCoordinates(int x, int y)
    {
        return new Vector2(Width, Height) * new Vector2(x, y);
    }

    public (int, int) GetIndex(Vector2 position)
    {
        return (
            (int)position.X / Width,
            (int)position.Y / Height
        );
    }
}
