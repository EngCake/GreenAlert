using Game.Entities;
using Game.Scenes;
using Microsoft.Xna.Framework;
using Nez;
using Nez.Sprites;
using Nez.Textures;
using System.Collections;

namespace Game.Tiles;

internal class Tile : Entity, IUpdatable, IEnumerable<TileElement>
{
    public static readonly int Layers = 6;

    public static readonly int GroundIndex = 0;
    public static readonly int PathIndex = 1;
    public static readonly int DecorationIndex = 2;
    public static readonly int WallIndex = 3;
    public static readonly int EntityIndex = 4;

    private readonly List<TileElement?> tileElements = new(new TileElement[Layers]);
    private TilesContainer? tiles;
    private SpriteRenderer? selectBox;

    public int LightsCount = 0;

    public override void Update()
    {
        base.Update();
        var color = LightsCount > 0 ? Color.White : Color.Black;
        foreach (var tileElement in this)
        {
            tileElement.Animator!.Color = color;
        }
        if (CheckIfSelectable())
        {
            selectBox!.Enabled = true;
            if (Input.LeftMouseButtonPressed)
            {
                Decoration = null;
            }
        }
        else
        {
            selectBox!.Enabled = false;
        }
    }

    public bool CheckIfSelectable()
    {
        return
            LightsCount > 0 && 
            Ground.IsPlantable && 
            Entity is null && 
            Path is null && 
            Wall is null && 
            Vector2.Distance(((MainScene)Scene).Player.Position, Position) < Constants.Player.Reach &&
            Bounds.Contains(Scene.Camera.MouseToWorldPoint());
    }

    public Rectangle Bounds => new RectangleF(Position - tiles!.TileSize / 2, tiles!.TileSize);

    public override void OnAddedToScene()
    {
        base.OnAddedToScene();
        tiles = Scene.GetSceneComponent<TilesContainer>();
        var texture = Scene.Content.LoadTexture(Content.UI.Select);
        selectBox = AddComponent(new SpriteRenderer(texture));
        selectBox.RenderLayer = Constants.RenderingLayers.Select;
        selectBox.Enabled = false;
    }

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
            tileElements[index] = value;
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
            else if (Decoration is not null)
            {
                Scene.Entities.Remove(Decoration);
                Decoration.RemoveAllComponents();
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
            else if (Entity is not null)
            {
                Scene.Entities.Remove(Entity);
                Entity.RemoveAllComponents();
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
