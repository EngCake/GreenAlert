using Game.Animation;
using Game.Entities;
using Game.Tiles;
using LDtk;
using LDtkTypes;
using Microsoft.Xna.Framework.Graphics;
using Nez;
using Nez.Sprites;
using Nez.Textures;
using System.Text.Json;

namespace Game.Loaders;

internal class LDtkLoader : SceneComponent
{
    #region LayersIdentifiers
    private const string GroundLayer = "Ground";

    private const string PathLayer = "Path";

    private const string DecorationsLayer = "Decorations";

    private const string WallsLayer = "Wall";

    private const string EntitiesLayer = "Entities";
    #endregion

    #region EntitiesIdentifiers
    private const string Chest = "Chest";
    private const string CrystalTree = "CrystalTree";
    private const string Obstacle = "Obstacle";
    private const string Player = "Player";
    #endregion

    private Texture2D? texture;
    private TilesContainer? tiles;

    public void LoadLevel()
    {
        var file = LDtkFile.FromFile(Content.Levels.Main);
        var world = file.LoadWorld(Worlds.World.Iid)!;
        var level = world.LoadLevel(Worlds.World.Level_0);

        for (var i = level.LayerInstances!.Length - 1; i >= 0; i--)
        {
            var layer = level.LayerInstances![i];

            if (tiles is null)
            {
                int width = layer._GridSize;
                int height = layer._GridSize;
                int rows = layer._CHei;
                int cols = layer._CWid;
                tiles = Scene.AddSceneComponent(new TilesContainer(rows, cols, width, height));
            }

            if (layer._Identifier == EntitiesLayer)
            {
                AddEntities(layer);
            }
            else
            {
                texture = texture ?? GetLayerTexture(level, layer);
                switch (layer._Identifier)
                {
                    case GroundLayer:
                        RenderTiles<Ground>(layer);
                        MarkGrass(layer.IntGridCsv);
                        break;
                    case PathLayer:
                        RenderTiles<GroundPath>(layer);
                        break;
                    case DecorationsLayer:
                        RenderTiles<Decoration>(layer);
                        break;
                    case WallsLayer:
                        RenderTiles<Wall>(layer);
                        break;
                }
            }
        }
    }

    private Texture2D GetLayerTexture(LDtkLevel level, LayerInstance layer)
    {
        var directory = Path.GetDirectoryName(level.FilePath);
        var path = Path.Join(directory, layer._TilesetRelPath);
        return Scene.Content.LoadTexture(path);
    }

    private void RenderTiles<T>(LayerInstance layer) where T : TileElement, new()
    {
        ArgumentNullException.ThrowIfNull(tiles);
        ArgumentNullException.ThrowIfNull(texture);

        var tilesInstances = layer.AutoLayerTiles.Length > 0 ? layer.AutoLayerTiles : layer.GridTiles;
        foreach (var tile in tilesInstances)
        {
            var animationDictionary = new AnimationDictionary();
            var rect = new RectangleF(tile.Src.X, tile.Src.Y, layer._GridSize, layer._GridSize);
            var sprite = new Sprite(texture, rect);
            var animation = new SpriteAnimation([sprite], Constants.FPS);
            animationDictionary["tile"] = animation;
            var newTile = new T();
            newTile.AnimationDictionary = animationDictionary;
            tiles[tile.Px.ToVector2()][newTile.TileIndex] = newTile;
        }
    }

    private void MarkGrass(int[] intGrid)
    {
        for (var i = 0; i < tiles!.Cols; i++)
        {
            for (var j = 0; j < tiles.Rows; j++)
            {
                tiles[i, j].Ground.IsPlantable = (intGrid[i * tiles.Cols + j] == 1) || (intGrid[i * tiles.Cols + j] == 2);
            }
        }
    }

    #region Adding entities
    private void AddEntities(LayerInstance layer)
    {
        foreach (var entity in layer.EntityInstances)
        {
            switch (entity._Identifier)
            {
                case Chest:
                    RenderChest(entity);
                    break;
                case CrystalTree:
                    RenderCrystalTree(entity);
                    break;
                case Obstacle:
                    RenderObstacle(entity);
                    break;
                case Player:
                    RenderPlayer(entity);
                    break;
            }
        }
    }

    private void RenderChest(EntityInstance entity)
    {
        ArgumentNullException.ThrowIfNull(tiles);
        ArgumentNullException.ThrowIfNull(texture);

        var fields = FieldsToDictionary(entity.FieldInstances);

        var animationDictionary = new AnimationDictionary() {
            { "closed", new SpriteAnimation([SpriteFromField(fields["ClosedSprite"])], Constants.FPS) },
            { "opened", new SpriteAnimation([SpriteFromField(fields["OpenedSprite"])], Constants.FPS) },
        };
        animationDictionary.DefaultAnimation = "closed";

        var chestEntity = new Entities.Chest();
        chestEntity.AnimationDictionary = animationDictionary;
        tiles[entity.Px.ToVector2()].Entity = chestEntity;
    }

    private void RenderCrystalTree(EntityInstance entity)
    {
        ArgumentNullException.ThrowIfNull(tiles);
        ArgumentNullException.ThrowIfNull(texture);

        var fields = FieldsToDictionary(entity.FieldInstances);
        var lightRadius = FromField<int>(fields["LightRadius"]);
        var animationDictionary = new AnimationDictionary() {
            { "tree", new SpriteAnimation([SpriteFromField(fields["Sprite"])], Constants.FPS) },
        };

        var crystalTree = new Entities.CrystalTree(lightRadius);
        crystalTree.AnimationDictionary = animationDictionary;

        tiles[entity.Px.ToVector2()].Entity = crystalTree;
    }

    private void RenderObstacle(EntityInstance entity)
    {
        ArgumentNullException.ThrowIfNull(tiles);
        ArgumentNullException.ThrowIfNull(texture);

        var fields = FieldsToDictionary(entity.FieldInstances);
        var animationDictionary = new AnimationDictionary() {
            { "obstacle", new SpriteAnimation([SpriteFromField(fields["Sprite"])], Constants.FPS) },
        };

        var obstacle = new Entities.Obstacle();
        obstacle.AnimationDictionary = animationDictionary;

        tiles[entity.Px.ToVector2()].Entity = obstacle;
    }

    private void RenderPlayer(EntityInstance entity)
    {
        ArgumentNullException.ThrowIfNull(tiles);
        ArgumentNullException.ThrowIfNull(texture);

        var fields = FieldsToDictionary(entity.FieldInstances);
        var animationDictionary = new AnimationDictionary() {
            { "idle", new SpriteAnimation([SpriteFromField(fields["Idle1"]), SpriteFromField(fields["Idle2"])], Constants.FPS) },
            { "walking", new SpriteAnimation(
                    [
                        SpriteFromField(fields["Walking1"]),
                        SpriteFromField(fields["Walking2"]),
                        SpriteFromField(fields["Walking3"]),
                        SpriteFromField(fields["Walking4"]),
                    ],
                    Constants.FPS
                )
            },
        };
        animationDictionary.DefaultAnimation = "idle";

        var player = new Entities.Player();
        player.AnimationDictionary = animationDictionary;

        tiles[entity.Px.ToVector2()].Entity = player;
    }
    #endregion

    #region Helpers
    private Dictionary<string, FieldInstance> FieldsToDictionary(FieldInstance[] fields)
    {
        var result = new Dictionary<string, FieldInstance>();
        foreach (var field in fields)
        {
            result[field._Identifier] = field;
        }
        return result;
    }

    private Sprite SpriteFromField(FieldInstance field)
    {
        var tile = ((JsonElement)field._Value).Deserialize<TilesetRectangle>();
        var sprite = new Sprite(texture, tile!);
        return sprite;
    }

    private T FromField<T>(FieldInstance field)
    {
        return ((JsonElement)field._Value).Deserialize<T>()!;
    }
    #endregion
}
