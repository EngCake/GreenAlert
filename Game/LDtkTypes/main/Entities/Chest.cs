// This file was automatically generated, any modifications will be lost!
#pragma warning disable
namespace LDtkTypes;

using LDtk;

using Microsoft.Xna.Framework;

public class Chest : ILDtkEntity
{
    public string Identifier { get; set; }
    public System.Guid Iid { get; set; }
    public int Uid { get; set; }
    public Vector2 Position { get; set; }
    public Vector2 Size { get; set; }
    public Vector2 Pivot { get; set; }
    public Rectangle Tile { get; set; }

    public Color SmartColor { get; set; }

    public TilesetRectangle? OpenedSprite { get; set; }
    public TilesetRectangle? ClosedSprite { get; set; }
    public bool IsLocked { get; set; }
    public string? KeyID { get; set; }
    public Loot[] Loot { get; set; }
    public string[] ContainedKeys { get; set; }
}
#pragma warning restore
