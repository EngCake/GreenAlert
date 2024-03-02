using Game.Components;
using Game.Loaders;
using Game.Tiles;
using Microsoft.Xna.Framework;
using Nez;

namespace Game.Scenes;

internal class MainScene : Scene
{
    public TilesContainer? Grid { get; private set; }

    private LDtkLoader? ldtkLoader;

    public MainScene()
    {
        ClearColor = new Color(0x2B2240);
        SetDesignResolution(Constants.Window.Width, Constants.Window.Height, SceneResolutionPolicy.ShowAllPixelPerfect);
        ldtkLoader = AddSceneComponent<LDtkLoader>();
    }

    public override void OnStart()
    {
        ldtkLoader!.LoadLevel();
        Grid = GetSceneComponent<TilesContainer>();
        Camera.RawZoom = 4;
        Camera.Position = Grid.Bounds.Center;
        AddSceneComponent(new MouseControl(Grid.Bounds));
    }
}
