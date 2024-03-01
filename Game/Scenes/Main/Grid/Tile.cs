using Nez;
using System.Collections;

namespace Game.Scenes.Main.Grid;

internal class Tile : Entity, IEnumerable<GridElement> {
    private static readonly int Layers = 5;
    private static readonly int GroundIndex = 0;
    private static readonly int PlantIndex = 4;

    private readonly List<GridElement> gridElements = new(new GridElement[Layers]);

    public GridElement? Ground {
        get => gridElements[GroundIndex];
        set {
            if (value is not null) {
                gridElements[GroundIndex] = value;
            }
        }
    }

    public GridElement? Plant {
        get => gridElements[PlantIndex];
        set {
            if (value is not null) {
                gridElements[PlantIndex] = value;
            }
        }
    }

    public override void OnAddedToScene() {
        foreach (var gridElement in gridElements) {
            if (gridElement is null) {
                continue;
            }
            gridElement.Position = Position;
            Scene.AddEntity(gridElement);
        }
    }

    public IEnumerator<GridElement> GetEnumerator() {
        return gridElements.Where(el => el is not null).GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator() {
        return gridElements.GetEnumerator();
    }
}
