using Nez.Textures;
using Nez;
using Game.Scenes.Main.Light;
using Game.Scenes.Main.Grid;

namespace Game.Scenes.Main.Ground;

internal class Boulder : GridElement {
    public Boulder() : base(2) {
        AddComponent<LightObstruction>();
    }

    public override void OnAddedToScene() {
        Sprites = [
            new Sprite(Scene.Content.LoadTexture(Content.Sprites.Tiles.Boulder))
        ];
        base.OnAddedToScene();
    }
}
