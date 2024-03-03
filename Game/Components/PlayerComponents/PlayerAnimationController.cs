using Nez.Sprites;
using Nez;
using Game.Entities;

namespace Game.Components.PlayerComponents;

internal class PlayerAnimationController : Component, IUpdatable
{
    private Player? player;
    private SpriteAnimator? animator;

    public override void OnAddedToEntity()
    {
        player = (Player)Entity;
        animator = player.Animator;
    }

    public void Update()
    {
        var playerDirection = player!.Direction;
        var animationName = player!.Velocity.Length() == 0 ? "idle" : "walking";
        var flipX = playerDirection.X == 1;
        if (animationName is not null && (animator!.CurrentAnimationName != animationName || animator!.FlipX != flipX))
        {
            animator!.Play(animationName);
            animator!.FlipX = flipX;
        }
    }
}
