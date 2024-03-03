using Microsoft.Xna.Framework;

namespace Game.Entities;

internal interface IUprightEntity
{
    int OriginalRenderLayer {  get; }

    Vector2 Center { get; }
}
