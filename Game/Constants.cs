namespace Game;

internal static class Constants
{
    public static readonly int FPS = 2;

    public static class Window
    {
        public static readonly string Title = "Green Cave";
        public static readonly int Width = 1920;
        public static readonly int Height = 1080;
    }

    public static class RenderingLayers
    {
        public static readonly int Ground = 7;
        public static readonly int Path = 6;
        public static readonly int Decoration = 5;

        public static readonly int WallsAndEntities = 4;
        public static readonly int Select = 3;
        public static readonly int Player = 2;
        public static readonly int BehindPlayer = 1;
    }

    public static class PhysicsLayers
    {
        public static readonly int LightBlockingCollider = 1;
        public static readonly int NonLightBlockingCollider = 2;
        public static readonly int Lightable = 3;
    }

    public static class Camera
    {
        public static readonly float Mass = 0.003f;
    }

    public static class Player
    {
        public static readonly float Mass = 0.00025f;
        public static readonly float Friction = 0.002f;
        public static readonly int Reach = 35;
    }
}
