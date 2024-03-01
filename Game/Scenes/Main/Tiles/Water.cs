using Microsoft.Xna.Framework;
using Nez;
using Nez.Sprites;
using Nez.Textures;

namespace Game.Scenes.Main.Tiles;

internal class Water : Entity {
    private static List<Sprite>? sprites;

    public override void OnAddedToScene() {
        var tiles = Scene.Content.LoadTexture(Content.Sprites.Tiles.Placeholder);
        if (sprites == null) {
            sprites = Sprite.SpritesFromAtlas(tiles, Constants.CellWidth, Constants.CellHeight);
        }
        var animator = AddComponent<SpriteAnimator>();
        animator.SetOrigin(Vector2.Zero);
        animator.AddAnimation("watering", 1, sprites[32 * 8 + 1], sprites[32 * 8 + 2]);
        animator.Play("watering");
    }
}
