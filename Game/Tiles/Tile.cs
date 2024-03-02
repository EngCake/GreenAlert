using Game.Entities;
using Nez;
using System.Collections;

namespace Game.Tiles;

internal class Tile : Entity, IEnumerable<TileElement>
{
    private static readonly int Layers = 6;

    public static readonly int GroundIndex = 0;
    public static readonly int PathIndex = 1;
    public static readonly int DecorationIndex = 2;
    public static readonly int WallIndex = 3;
    public static readonly int EntityIndex = 4;

    private readonly List<TileElement?> tileElements = new(new TileElement[Layers]);

    #region Getters and Setters
    public TileElement? this[int index]
    {
        get => tileElements[index];
        set
        {
            if (value is not null)
            {
                value.Position = Position;
                Scene.AddEntity(value);
            }
            tileElements[GroundIndex] = value;
        }
    }

    public Ground Ground
    {
        get => (Ground)(tileElements[GroundIndex] ?? throw new ArgumentNullException("Attempting to access null Ground"));
        set
        {
            value.Position = Position;
            Scene.AddEntity(value);
            tileElements[GroundIndex] = value;
        }
    }

    public GroundPath? Path
    {
        get => (GroundPath?)tileElements[PathIndex];
        set
        {
            if (value is not null)
            {
                value.Position = Position;
                Scene.AddEntity(value);
            }
            tileElements[GroundIndex] = value;
        }
    }

    public Decoration? Decoration
    {
        get => (Decoration?)tileElements[DecorationIndex];
        set
        {
            if (value is not null)
            {
                value.Position = Position;
                Scene.AddEntity(value);
            }
            tileElements[DecorationIndex] = value;
        }
    }

    public Wall? Wall
    {
        get => (Wall?)tileElements[WallIndex];
        set
        {
            if (value is not null)
            {
                value.Position = Position;
                Scene.AddEntity(value);
            }
            tileElements[WallIndex] = value;
        }
    }

    public TileElement? Entity
    {
        get => tileElements[EntityIndex];
        set
        {
            if (value is not null)
            {
                value.Position = Position;
                Scene.AddEntity(value);
            }
            tileElements[EntityIndex] = value;
        }
    }
    #endregion

    #region Enumarator Implementation
    public IEnumerator<TileElement> GetEnumerator()
    {
        return tileElements.Where(el => el is not null).GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return tileElements.GetEnumerator();
    }
    #endregion
}
