using System;
using UnityEngine;

public class PlacementSystem : MonoBehaviour
{
    [SerializeField] private GameObject mouseIndicator;
    [SerializeField] private GameObject cellIndicator;

    [SerializeField] private InputManager inputManager;

    [SerializeField] private Grid grid;

    [SerializeField] private ObjectDataBaseSO dataBase;
    private int selectedObjectIndex = -1;

    [SerializeField] private GameObject gridVisualization;

    private void Start()
    {
        StopPlacement();
       /* floorData = new();
        furintureData = new();
        prewviewRenderer = cellIndicator.GetComponentInChildren<Renderer>();*/
    }

    public void StartPlacement(int ID)
    {
        StopPlacement();
        selectedObjectIndex = dataBase.objectData.FindIndex(data => data.ID == ID);
        if (selectedObjectIndex > 0)
        {
            Debug.LogError($"No ID found {ID}");
            return;
        }
        gridVisualization.SetActive( true );
        cellIndicator.SetActive( true );
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

        //bool placementValidity = CheckPlacementValidity(gridPosition, selectedObjectIndex);

        /*if (placementValidity == false)
        {
            //source.PlayOneShot(wrongPlacementClip); // Poner Sonido clip de malo osea que no se puede 
            return;
        }*/
        //source.PlayOneShot(correctPlacementClip); // Poner Sonido clip de bueno osea que si se puede 

        GameObject newObject = Instantiate(dataBase.objectData[selectedObjectIndex].prefab);
        newObject.transform.position = grid.WorldToCell(gridPosition);
       /* placedGameObjects.Add(newObject);

        GridData selectedData = dataBase.objectData[selectedObjectIndex].ID == 0 ?
            floorData:
            furnitureData:
        selectedData.AddObjectAt(gridPosition)*/

    }

    private bool CheckPlacementValidity(Vector3Int gridPosition, int selectedObjectIndex)
    {
        throw new NotImplementedException();
    }

    private void StopPlacement()
    {
        selectedObjectIndex = -1;
        gridVisualization.SetActive(false);
        cellIndicator.SetActive(false);
        inputManager.OnClicked -= PlaceStructure;
        inputManager.OnExit -= StopPlacement;

    }

    private void Update()
    {
        if (selectedObjectIndex < 0)
            return;
        Vector3 mousePosition = inputManager.GetSelectedMapPosition();
        Vector3Int gridPosition = grid.WorldToCell(mousePosition);
        mouseIndicator.transform.position = mousePosition;
        cellIndicator.transform.position = grid.CellToWorld(gridPosition);
    }
}
