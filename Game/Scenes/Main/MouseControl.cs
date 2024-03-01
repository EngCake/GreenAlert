using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Nez;

namespace Game.Scenes.Main;

internal class MouseControl : SceneComponent {
    private readonly RectangleF bounds;
    private VirtualButton moveCamera;

    private Vector2 previousPosition;

    public MouseControl(RectangleF cameraBounds) {
        SetUpdateOrder(int.MaxValue);
        bounds = cameraBounds;

        moveCamera = new VirtualButton();
        moveCamera.AddMouseRightButton();
        moveCamera.AddMouseMiddleButton();
    }

    public override void Update() {
        if (moveCamera.IsDown && IsMouseInsideWindow()) {
            var delta = Scene.Camera.MouseToWorldPoint() - previousPosition;
            Scene.Camera.Position -= delta;
            ClipCameraInsideBounds();
        }
        previousPosition = Scene.Camera.MouseToWorldPoint();
    }

    private void ClipCameraInsideBounds() {
        if (!bounds.Contains(Scene.Camera.Bounds)) {
            var cameraPosition = Scene.Camera.Position;
            if (cameraPosition.X > bounds.Right - Scene.Camera.Bounds.Width / 2) {
                cameraPosition.X = bounds.Right - Scene.Camera.Bounds.Width / 2;
            }
            else if (cameraPosition.X < bounds.Left + Scene.Camera.Bounds.Width / 2) {
                cameraPosition.X = bounds.Left + Scene.Camera.Bounds.Width / 2;
            }

            if (cameraPosition.Y < bounds.Top + Scene.Camera.Bounds.Height / 2) {
                cameraPosition.Y = bounds.Top + Scene.Camera.Bounds.Height / 2;
            }
            else if (cameraPosition.Y > bounds.Bottom - Scene.Camera.Bounds.Height / 2) {
                cameraPosition.Y = bounds.Bottom - Scene.Camera.Bounds.Height / 2;
            }
            Scene.Camera.Position = cameraPosition;
        }
    }

    private bool IsMouseInsideWindow() {
        return Core.GraphicsDevice.Viewport.Bounds.Contains(Input.MousePosition);
    }
}

