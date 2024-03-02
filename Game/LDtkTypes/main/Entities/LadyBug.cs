// This file was automatically generated, any modifications will be lost!
#pragma warning disable
namespace LDtkTypes;

using LDtk;

using Microsoft.Xna.Framework;

public class LadyBug : ILDtkEntity
{
    public string Identifier { get; set; }
    public System.Guid Iid { get; set; }
    public int Uid { get; set; }
    public Vector2 Position { get; set; }
    public Vector2 Size { get; set; }
    public Vector2 Pivot { get; set; }
    public Rectangle Tile { get; set; }

    public Color SmartColor { get; set; }

    public TilesetRectangle? Sprite { get; set; }
    public int HP { get; set; }
    public int DamageValue { get; set; }
    public float Cooldown { get; set; }
    public float SeedDropRate { get; set; }
    public float HeartDropRate { get; set; }
    public float ManaDropRate { get; set; }
}
#pragma warning restore
