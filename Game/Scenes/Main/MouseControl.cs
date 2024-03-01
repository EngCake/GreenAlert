using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Nez;
using Nez.Tweens;

namespace Game.Scenes.Main;

internal class MouseControl : SceneComponent, IUpdatable {
    private readonly RectangleF bounds;
    private VirtualButton button;

    private Vector2 previousPosition;

    public MouseControl(RectangleF boundaries) {
        SetUpdateOrder(int.MaxValue);
        this.bounds = boundaries;
        button = new VirtualButton();
        button.AddMouseRightButton();
    }

    public override void Update() {
        if (button.IsDown) {
            var delta = Scene.Camera.MouseToWorldPoint() - previousPosition;
            var newCameraPosition = Scene.Camera.Position - delta;
            var newCameraBounds = new RectangleF(newCameraPosition, Scene.Camera.Bounds.Size);
            if (bounds.Contains(newCameraBounds)) {
                Scene.Camera.Position -= delta;
            }
        }
        previousPosition = Scene.Camera.MouseToWorldPoint();
    }
}
