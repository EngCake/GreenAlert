using Microsoft.Xna.Framework;
using Nez;

namespace Game.Scenes;

internal abstract class BaseScene : Scene {
    public BaseScene() {
        ClearColor = Color.White;
        SetDesignResolution(Constants.Window.Width, Constants.Window.Height, SceneResolutionPolicy.ShowAllPixelPerfect);
    }
}
