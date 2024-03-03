using Game.Scenes;
using Nez;

namespace Game;

internal abstract class BaseEntity : Entity
{
    public override void OnAddedToScene()
    {
        ((MainScene)Scene).UpdateLights();
    }

    public override void OnRemovedFromScene()
    {
        ((MainScene)Scene).UpdateLights();
    }
}
