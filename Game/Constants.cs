namespace Game;

internal static class Constants
{
    public static class Window
    {
        public static readonly string Title = "Green Cave";
        public static readonly int Width = 1920;
        public static readonly int Height = 1080;
    }

    public static readonly int FPS = 2;

    public static class RenderingLayers
    {
        public static readonly int Ground = 6;
        public static readonly int Path = 5;
        public static readonly int Decoration = 4;
        // Layers 2, 3  Reserved for any other use
        public static readonly int Wall = 1;
        public static readonly int Entities = 0;
    }
}
