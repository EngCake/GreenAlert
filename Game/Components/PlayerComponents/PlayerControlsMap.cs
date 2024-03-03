using Microsoft.Xna.Framework.Input;
using Nez;

namespace Game.Components.PlayerComponents;
internal class PlayerControlsMap : Component
{
    public VirtualJoystick Movement { get; private set; }

    public PlayerControlsMap()
    {
        Movement = new VirtualJoystick(true);
        Movement.AddKeyboardKeys(VirtualInput.OverlapBehavior.TakeNewer, Keys.A, Keys.D, Keys.W, Keys.S);
        Movement.AddKeyboardKeys(VirtualInput.OverlapBehavior.TakeNewer, Keys.Left, Keys.Right, Keys.Up, Keys.Down);
    }
}