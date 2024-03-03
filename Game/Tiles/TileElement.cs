using Game.Animation;
using Game.Entities;
using Game.Scenes;
using Nez;
using Nez.Sprites;

namespace Game.Tiles;

internal abstract class TileElement : Entity, IUpdatable
{
    public AnimationDictionary? AnimationDictionary;

    public SpriteAnimator? Animator;

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

        Tiles = Scene.GetSceneComponent<TilesContainer>();
    }

    public override void Update()
    {
        base.Update();
        var player = ((MainScene)Scene).Player;
        if (this is IUprightEntity uprightEntity)
        {
            RenderLayer = uprightEntity.Center.Y < player.Position.Y ? uprightEntity.OriginalRenderLayer : Constants.RenderingLayers.BehindPlayer;
        }
    }

    public abstract int TileIndex { get; }

    public int RenderLayer
    {
        get => Animator!.RenderLayer;
        set => Animator!.RenderLayer = value;
    }
}
