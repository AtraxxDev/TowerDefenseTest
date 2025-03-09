using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

public class PlacementSystem : MonoBehaviour
{

    [SerializeField] private InputManager inputManager;

    [SerializeField] private Grid grid;

    [SerializeField] private ObjectDataBaseSO dataBase;

    [SerializeField] private GameObject gridVisualization;

    [SerializeField] private PreviewColorSystem preview;

    private GridData floorData;
    private GridData furnitureData;

    private Vector3Int lastDetectedPosition = Vector3Int.zero;

    [SerializeField] private ObjectPlacer objectPlacer;

    IBuildingState buildingState;

    [SerializeField] private SoundFeedback soundFeedback;


    private void Start()
    {
        StopPlacement();
        floorData = new();
        furnitureData = new();
    }

    public void StartPlacement(int ID)
    {
        StopPlacement();
        gridVisualization.SetActive( true );
        buildingState = new PlacementState(ID, grid, preview, dataBase, floorData, furnitureData, objectPlacer,soundFeedback);
        inputManager.OnClicked += PlaceStructure;
        inputManager.OnExit += StopPlacement;
        if (buildingState is PlacementState placementState)
        {
            inputManager.OnRotate += placementState.RotateObject;
        }

    }

    public void StartRemoving()
    {
        StopPlacement();
        gridVisualization.SetActive(true);
        buildingState = new RemovingState(grid, preview, floorData, furnitureData, objectPlacer);
        inputManager.OnClicked += PlaceStructure;
        inputManager.OnExit += StopPlacement;
    }

    private void PlaceStructure()
    {
        if (inputManager.IsPointerIsOverUI())
        {
            return;
        }

        Vector3 mousePosition = inputManager.GetSelectedMapPosition();
        Vector3Int gridPosition = grid.WorldToCell(mousePosition);

        buildingState.OnAction(gridPosition);

    }

    //private bool CheckPlacementValidity(Vector3Int gridPosition, int selectedObjectIndex)
    //{
    //   GridData selectedData = dataBase.objectData[selectedObjectIndex].ID == 0 ? floorData : furnitureData;

    //    return selectedData.CanPlaceObjectAt(gridPosition, dataBase.objectData[selectedObjectIndex].Size);
    //}

    private void StopPlacement()
    {
        if (buildingState == null)
            return;
        gridVisualization.SetActive(false);
        buildingState.EndState();
        inputManager.OnClicked -= PlaceStructure;
        inputManager.OnExit -= StopPlacement;

        if (buildingState is PlacementState placementState)
        {
            inputManager.OnRotate -= placementState.RotateObject;
        }

        lastDetectedPosition = Vector3Int.zero;
        buildingState = null;

    }

    private void Update()
    {
        if (buildingState == null)
            return;
        Vector3 mousePosition = inputManager.GetSelectedMapPosition();
        Vector3Int gridPosition = grid.WorldToCell(mousePosition);

        if (lastDetectedPosition != gridPosition)
        {
            buildingState.UpdateState(gridPosition);
            lastDetectedPosition = gridPosition;
        }
    }
}
