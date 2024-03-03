using Game.Tiles;
using Game.Utils;
using Microsoft.Xna.Framework;
using Nez;
using System.Reflection.Metadata.Ecma335;

namespace Game.Components;

internal class Mover : Component, IUpdatable
{
    [Inspectable]
    private float mass;
    [Inspectable]
    private float friction;

    private IMovable? movable;
    private ColliderShape? colliderShape;
    private TilesContainer? tiles;

    public bool AllowMovementInDarkness;

    public Mover(bool allowMovementInDarkness)
    {
        mass = Constants.Player.Mass;
        friction = Constants.Player.Friction;
        AllowMovementInDarkness = allowMovementInDarkness;
    }

    public override void OnAddedToEntity()
    {
        if (Entity is IMovable movable)
        {
            this.movable = movable;
            colliderShape = Entity.GetComponent<ColliderShape>();
            tiles = Entity.Scene.GetSceneComponent<TilesContainer>();
        }
        else
        {
            throw new Exception("Mover must only be added to an IMovable entity");
        }
    }

    public void Update()
    {
        var force = movable!.Force;
        movable.Acceleration = force / mass - force * friction;
        movable.Velocity = movable.Acceleration * Time.DeltaTime;

        Entity.Position += movable.Velocity * Time.DeltaTime;

        // Check for collision
        if (colliderShape!.CollidesWithAny(out var result))
        {
            Entity.Position -= result.MinimumTranslationVector;
            movable.Velocity = Vector2.Zero;
        }

        if (!AllowMovementInDarkness)
        {
            var (x, y) = tiles!.GetIndex(Entity.Position);
            var size = new Vector2(tiles.Width, tiles.Height);
            var currentTileBounds = new RectangleF(Entity.Position - size / 2, size);
            for (var i = -1; i <= 1; i++)
            {
                for (var j = -1; j <= 1; j++)
                {
                    if (i == 0 && j == 0)
                    {
                        continue;
                    }
                    var neighboringTile = tiles[x + i, y + j];
                    var neighboringTileBounds = new RectangleF(neighboringTile.Position - size / 2, size);
                    if (currentTileBounds.Intersects(neighboringTileBounds) && neighboringTile.LightsCount == 0)
                    {
                        Entity.Position -= neighboringTileBounds.GetMinimumTranslationVector(currentTileBounds);
                        movable.Velocity = Vector2.Zero;
                    }
                }
            }
        }

        var playerDirection = Vector2.Round(Vector2.Normalize(movable.Velocity));
        if (movable.Velocity != Vector2.Zero)
        {
            movable.Direction = playerDirection;
        }
    }
}
