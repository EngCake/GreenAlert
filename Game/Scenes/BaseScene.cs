using Microsoft.Xna.Framework;
using Nez;

namespace Game.Scenes;

internal abstract class BaseScene : Scene {
    public BaseScene() {
        SetDesignResolution(1920, 1080, SceneResolutionPolicy.ShowAllPixelPerfect);
        ClearColor = Color.White;
    }
}
