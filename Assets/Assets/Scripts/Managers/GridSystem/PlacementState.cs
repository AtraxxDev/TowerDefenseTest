using UnityEngine;

public class PlacementState : IBuildingState
{
    private int selectedObjectIndex = -1;
    int ID;
    private Quaternion currentRotation = Quaternion.identity;
    Grid grid;
    PreviewColorSystem previewColorSystem;
    ObjectDataBaseSO dataBase;
    GridData floorData;
    GridData furnitureData;
    ObjectPlacer objectPlacer;
    SoundFeedback soundFeedback;

    public PlacementState(int iD, Grid grid, PreviewColorSystem previewColorSystem, ObjectDataBaseSO dataBase, GridData floorData, GridData furnitureData, ObjectPlacer objectPlacer, SoundFeedback soundFeedback)
    {
        ID = iD;
        this.grid = grid;
        this.previewColorSystem = previewColorSystem;
        this.dataBase = dataBase;
        this.floorData = floorData;
        this.furnitureData = furnitureData;
        this.objectPlacer = objectPlacer;
        this.soundFeedback = soundFeedback;

        selectedObjectIndex = dataBase.objectData.FindIndex(data => data.ID == ID);
        if (selectedObjectIndex > -1)
        {
            previewColorSystem.StartShowingPlacementPreview(dataBase.objectData[selectedObjectIndex].prefab, dataBase.objectData[selectedObjectIndex].Size);
        }
        else
            throw new System.Exception($"No object whith ID: {iD}");
        this.soundFeedback = soundFeedback;
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
            soundFeedback.PlaySFXSound(SFXType.wrongPlacement);
            return;
        }
        soundFeedback.PlaySFXSound(SFXType.Place);

        int index = objectPlacer.PlaceObject(dataBase.objectData[selectedObjectIndex].prefab, grid.CellToWorld(gridPosition),currentRotation);


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
    public void RotateObject()
    {
        currentRotation *= Quaternion.Euler(0f, 90f, 0f); // Rota 90° en el eje Y
        previewColorSystem.UpdateRotation(currentRotation); // Actualiza la vista previa
    }
}
