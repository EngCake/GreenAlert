using Game.Components;
using Game.Components.Light;
using Game.Entities;
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
        ClearColor = Color.Black;
        SetDesignResolution(Constants.Window.Width, Constants.Window.Height, SceneResolutionPolicy.ShowAllPixelPerfect);
        ldtkLoader = AddSceneComponent<LDtkLoader>();
    }

    public override void Update()
    {
        base.Update();
    }

    public void UpdateLights()
    {
        Entities.EntitiesOfType<Tile>().ForEach(tile => tile.LightsCount = 0);
        Entities.FindComponentsOfType<LightSource>().ForEach(lightSource => lightSource.Light());
    }

    public override void OnStart()
    {
        ldtkLoader!.LoadLevel();
        Grid = GetSceneComponent<TilesContainer>();

        var player = Entities.EntitiesOfType<Player>().First();

        Camera.RawZoom = 4;
        Camera.Position = player.Position;
        AddSceneComponent(new MouseControl(Grid.Bounds));
    }

    private Player? player;
    public Player Player
    {
        get
        {
            player ??= Entities.EntitiesOfType<Player>().First();
            return player;
        }
    }
}
