using UnityEngine;

public class PlacementState : IBuildingState
{
    private int selectedObjectIndex = -1;
    int ID;
    Grid grid;
    PreviewColorSystem previewColorSystem;
    ObjectDataBaseSO dataBase;
    GridData floorData;
    GridData furnitureData;
    ObjectPlacer objectPlacer;

    public PlacementState(int iD, Grid grid, PreviewColorSystem previewColorSystem, ObjectDataBaseSO dataBase, GridData floorData, GridData furnitureData, ObjectPlacer objectPlacer)
    {
        ID = iD;
        this.grid = grid;
        this.previewColorSystem = previewColorSystem;
        this.dataBase = dataBase;
        this.floorData = floorData;
        this.furnitureData = furnitureData;
        this.objectPlacer = objectPlacer;

        selectedObjectIndex = dataBase.objectData.FindIndex(data => data.ID == ID);
        if (selectedObjectIndex > -1)
        {
            previewColorSystem.StartShowingPlacementPreview(dataBase.objectData[selectedObjectIndex].prefab, dataBase.objectData[selectedObjectIndex].Size);
        }
        else
            throw new System.Exception($"No object whith ID: {iD}");
    }

    public void EndState()
    {
        previewColorSystem.StopShowingPreview();

    }

    public void OnAction(Vector3Int gridPosition)
    {
        bool placementValidity = CheckPlacementValidity(gridPosition, selectedObjectIndex);

        if (placementValidity == false)
        {
            return;
        }

        int index = objectPlacer.PlaceObject(dataBase.objectData[selectedObjectIndex].prefab, grid.CellToWorld(gridPosition));


        GridData selectedData = dataBase.objectData[selectedObjectIndex].ID == 0 ?
            floorData :
            furnitureData;
        selectedData.AddObjectAt(gridPosition, dataBase.objectData[selectedObjectIndex].Size, dataBase.objectData[selectedObjectIndex].ID, index);

        previewColorSystem.UpdatePosition(grid.CellToWorld(gridPosition), false);
    }

    private bool CheckPlacementValidity(Vector3Int gridPosition, int selectedObjectIndex)
    {
        GridData selectedData = dataBase.objectData[selectedObjectIndex].ID == 0 ? floorData : furnitureData;

        return selectedData.CanPlaceObjectAt(gridPosition, dataBase.objectData[selectedObjectIndex].Size);
    }

    public void UpdateState(Vector3Int gridPosition)
    {
        bool placementValidity = CheckPlacementValidity(gridPosition, selectedObjectIndex);

        previewColorSystem.UpdatePosition(grid.CellToWorld(gridPosition), placementValidity);
    }
}
