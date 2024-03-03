using Microsoft.Xna.Framework;

namespace Game.Components;

internal interface IMovable
{
    /// <summary>
    /// Force applied to entity to move it
    /// </summary>
    Vector2 Force { get; set; }

    /// <summary>
    /// The direction the entity is facing
    /// </summary>
    Vector2 Direction { get; set; }

    /// <summary>
    /// The current position of the entity
    /// </summary>
    Vector2 Position { get; set; }

    /// <summary>
    /// The current velocity of the entity
    /// </summary>
    Vector2 Velocity { get; set; }

    /// <summary>
    /// The current acceleration of the entity
    /// </summary>
    Vector2 Acceleration { get; set; }
}
