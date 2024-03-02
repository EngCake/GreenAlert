using Game.Scenes;
using Nez;

namespace Game;

internal class GameClass : Core {
    public GameClass(): base(
        windowTitle: Constants.Window.Title,
        width: Constants.Window.Width,
        height: Constants.Window.Height
    ) {
    }

    protected override void BeginRun() {
        Screen.SetSize(Constants.Window.Width, Constants.Window.Height);
        Scene = new MainScene();
    }
}
