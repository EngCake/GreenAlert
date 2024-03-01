using Game.Scenes.Main.Grid;
using Game.Scenes.Main.Light;
using Nez;
using Nez.Sprites;
using Nez.Textures;

namespace Game.Scenes.Main.Ground;

internal class Soil : GridElement {

    public Soil() : base(2) {}

    public override void OnAddedToScene() {
        Sprites = [
            new Sprite(Scene.Content.LoadTexture(Content.Sprites.Tiles.Dirt))
        ];
        base.OnAddedToScene();
    }

#if DEBUG
    private SpriteAnimator? animator;

    public override void Update() {
        base.Update();
        if (animator is null) {
            animator = GetComponent<SpriteAnimator>();
        }
        if (Input.LeftMouseButtonPressed && animator.Bounds.Contains(Scene.Camera.MouseToWorldPoint())) {
            if (!HasComponent<LightSource>()) {
                AddComponent(new LightSource(150));
            }
        }
        else if (Input.RightMouseButtonPressed && animator.Bounds.Contains(Scene.Camera.MouseToWorldPoint())) {
            if (HasComponent<LightSource>()) {
                RemoveComponent<LightSource>();
            }
        }
    }
#endif
}
