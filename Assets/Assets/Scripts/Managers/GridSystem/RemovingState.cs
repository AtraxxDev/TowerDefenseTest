using System;
using UnityEngine;

public class RemovingState : IBuildingState
{
    private int gameObjectIndex = -1;
    Grid grid;
    PreviewColorSystem previewColorSystem;
    GridData firstCategoryData;
    GridData secondCategoryData;
    ObjectPlacer objectPlacer;

    public RemovingState(Grid grid, PreviewColorSystem previewColorSystem, GridData firstCategoryData, GridData secondCategoryData, ObjectPlacer objectPlacer)
    {
        this.grid = grid;
        this.previewColorSystem = previewColorSystem;
        this.firstCategoryData = firstCategoryData;
        this.secondCategoryData = secondCategoryData;
        this.objectPlacer = objectPlacer;

        previewColorSystem.StartShowingRemovePreview();
    }

    public void EndState()
    {
        previewColorSystem.StopShowingPreview();
    }

    public void OnAction(Vector3Int gridPosition)
    {
        GridData selectedData = null;
        if (secondCategoryData.CanPlaceObjectAt(gridPosition, Vector2Int.one) == false)
        {
            selectedData = secondCategoryData;
        }
        else if (firstCategoryData.CanPlaceObjectAt(gridPosition, Vector2Int.one) == false)
        {
            selectedData = firstCategoryData;

        }

        if (selectedData == null)
        {
            // sound 
        }
        else
        {
            gameObjectIndex = selectedData.GetRepresentationIndex(gridPosition);
            if (gameObjectIndex == -1)
                return;
            selectedData.RemoveObjectAt(gridPosition);
            objectPlacer.RemoveObjectAt(gameObjectIndex);
        }
        Vector3 cellPosition = grid.CellToWorld(gridPosition);
        previewColorSystem.UpdatePosition(cellPosition, CheckIsSelectionIsValid(gridPosition));
    }

    private bool CheckIsSelectionIsValid(Vector3Int gridPosition)
    {
        return !(secondCategoryData.CanPlaceObjectAt(gridPosition, Vector2Int.one) && firstCategoryData.CanPlaceObjectAt(gridPosition, Vector2Int.one));
    }

    public void UpdateState(Vector3Int gridPosition)
    {
        bool validity = CheckIsSelectionIsValid(gridPosition);
        previewColorSystem.UpdatePosition(grid.CellToWorld(gridPosition), validity);
    }
}
