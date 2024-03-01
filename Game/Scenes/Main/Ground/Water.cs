using Nez.Textures;
using Nez;
using Game.Scenes.Main.Grid;

namespace Game.Scenes.Main.Ground;

internal class Water : GridElement {
    public Water() : base(2) {}

    public override void OnAddedToScene() {
        Sprites = [
            new Sprite(Scene.Content.LoadTexture(Content.Sprites.Tiles.Water_0)),
            new Sprite(Scene.Content.LoadTexture(Content.Sprites.Tiles.Water_1)),
        ];
        base.OnAddedToScene();
    }
}
