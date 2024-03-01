using Nez;
using Nez.Sprites;
using Nez.Textures;

namespace Game.Scenes.Main.Tiles;

internal class Soil : Entity {
    public override void OnAddedToScene() {
        var tiles = Scene.Content.LoadTexture(Content.Sprites.Tiles.Placeholder);
        var sprites = Sprite.SpritesFromAtlas(tiles, 16, 16);
        AddComponent(new SpriteRenderer(sprites[6 * 32]));
    }
}
