using Nez;
using Nez.Sprites;

namespace Game.Scenes.Main.Tiles;

internal class CrystalTree : Entity {
    public override void OnAddedToScene() {
        var assset = Scene.Content.LoadTexture(Content.Sprites.Plants.Crystal_tree);
        var sprite = new SpriteRenderer(assset);
        AddComponent(sprite);
    }
}
