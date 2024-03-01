using Nez;
using Nez.Sprites;
using Nez.Textures;

namespace Game.Scenes.Main.Tiles;

internal enum SoilType {
    Brown,
    Orange,
    Blue
}

internal class Soil : Entity {
    private static List<Sprite>? sprites;

    public SoilType SoilType { get; private set; }

    public Soil(SoilType soilType) {
        SoilType = soilType;
    }

    public override void OnAddedToScene() {
        var tiles = Scene.Content.LoadTexture(Content.Sprites.Tiles.Placeholder);
        if (sprites == null ) {
            sprites = Sprite.SpritesFromAtlas(tiles, Constants.CellWidth, Constants.CellHeight);
        }
        var sprite = new SpriteRenderer(GetSpriteForType());
        AddComponent(sprite);
    }

    private Sprite GetSpriteForType() {
        switch (SoilType) {
            case SoilType.Brown: return sprites![6 * 32];

            case SoilType.Orange: return sprites![6 * 32 + 3];

            case SoilType.Blue: return sprites![6 * 32 + 6];
        }
        throw new ArgumentException($"Unsupported soil type given: {SoilType}");
    }
}
