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

    private const string WallsLayer = "Walls";

    private const string EntitiesLayer = "Entities";
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
                AddChests(layer);
            }
            else
            {
                texture = texture ?? GetLayerTexture(level, layer);
                switch (layer._Identifier)
                {
                    case GroundLayer:
                        RenderTiles<Ground>(layer);
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

    #region Adding entities
    private void AddChests(LayerInstance layer)
    {
        ArgumentNullException.ThrowIfNull(tiles);
        ArgumentNullException.ThrowIfNull(texture);

        foreach (var entity in layer.EntityInstances)
        {
            if (entity._Identifier != "Chest")
            {
                continue;
            }
            
            var animationDictionary = new AnimationDictionary();
            foreach (var field in entity.FieldInstances)
            {
                if (field._Identifier == "ClosedSprite")
                {
                    var tile = ((JsonElement)field._Value).Deserialize<TilesetRectangle>();
                    var sprite = new Sprite(texture, tile!);
                    var animation = new SpriteAnimation([sprite], Constants.FPS);
                    animationDictionary["opened"] = animation;
                }
            }

            var chestEntity = new Entities.Chest();
            chestEntity.AnimationDictionary = animationDictionary;
            tiles[entity.Px.ToVector2()].Entity = chestEntity;
        }
    }
    #endregion
}
