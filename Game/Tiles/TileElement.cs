using Game.Animation;
using Game.Components.Light;
using Nez;
using Nez.Sprites;

namespace Game.Tiles;

internal abstract class TileElement : Entity
{
    public AnimationDictionary? AnimationDictionary;

    protected SpriteAnimator? Animator;
    protected TilesContainer? Tiles;

    public override void OnAddedToScene()
    {
        ArgumentNullException.ThrowIfNull(AnimationDictionary);

        Animator = AddComponent<SpriteAnimator>();
        foreach (var (animationName, animation) in AnimationDictionary)
        {
            Animator.AddAnimation(animationName, animation);
        }
        Animator.Play(AnimationDictionary.DefaultAnimation ?? throw new NullReferenceException("All tile elements must have a default animation set"));

        AddComponent(new Lightable(Animator));

        Tiles = Scene.GetSceneComponent<TilesContainer>();
    }

    public abstract int TileIndex { get; }

    public int RenderLayer
    {
        get => Animator!.RenderLayer;
        set => Animator!.RenderLayer = value;
    }
}
