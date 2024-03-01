using Game.Scenes.Main.Light;
using Nez;
using Nez.Sprites;
using Nez.Textures;

namespace Game.Scenes.Main.Grid;

internal class GridElement : Entity
{
    private float animationFPS;
    protected Sprite[] Sprites;
    protected SpriteAnimator? Animator;

    public GridElement(float animationSpeed)
    {
        animationFPS = animationSpeed;
        Sprites = Array.Empty<Sprite>();
    }

    public override void OnAddedToScene()
    {
        Animator = AddComponent<SpriteAnimator>();
        Animator.AddAnimation("grid_element", Sprites, animationFPS);
        Animator.Play("grid_element");
        AddComponent(new Lightable(Animator));
    }
}
