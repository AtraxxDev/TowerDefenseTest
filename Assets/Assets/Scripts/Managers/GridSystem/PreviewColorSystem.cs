using System;
using UnityEngine;

public class PreviewColorSystem : MonoBehaviour
{
    [SerializeField] private float previewOffsetY = 0.06f;

    [SerializeField] private GameObject cellIndicator;
    private GameObject previewObject;

    [SerializeField] private Material previewMaterialsPrefab;
    private Material previewMaterialInstance;

    private Renderer cellIndicatorRenderer;

    private void Start()
    {
        previewMaterialInstance = new Material(previewMaterialsPrefab);
        cellIndicatorRenderer = cellIndicator.GetComponentInChildren<Renderer>();
        cellIndicator.SetActive(false);
    }

    public void StartShowingPlacementPreview(GameObject prefab,Vector2Int size)
    {
        previewObject = Instantiate(prefab);
        PreparePreview(previewObject);
        PrepareCursor(size);
        cellIndicator.SetActive(true);
    }

    private void PrepareCursor(Vector2Int size)
    {
        if (size.x > 0 || size.y > 0)
        {
            cellIndicator.transform.localScale = new Vector3(size.x, 1, size.y);
            cellIndicatorRenderer.material.mainTextureScale = size;
        }
    }

    private void PreparePreview(GameObject previewObject)
    {
        Renderer[] rederers = previewObject.GetComponentsInChildren<Renderer>();
        foreach (Renderer renderer in rederers)
        {
            Material[] materials = renderer.materials;
            for (int i = 0; i < materials.Length; i++)
            {
                materials[i] = previewMaterialInstance;
            }
            renderer.materials = materials;
        }
    }


    public void UpdatePosition(Vector3 position , bool validity)
    {
        if (previewObject != null)
        {
            MovePreview(position);
            ApplyFeebackToPreview(validity);
        }
        MoveCursor(position);
        ApplyFeebackToCursor(validity);
    }
    public void StopShowingPreview()
    {
        cellIndicator.SetActive(false);
        if (previewObject != null )
            Destroy(previewObject);
    }

    private void ApplyFeebackToPreview(bool validity)
    {
        Color c = validity ? Color.white : Color.red;
        c.a = 0.5f;
        previewMaterialInstance.color = c;
    }

    private void ApplyFeebackToCursor(bool validity)
    {
        Color c = validity ? Color.white : Color.red;
        c.a = 0.5f;
        cellIndicatorRenderer.material.color = c;
    }

    private void MoveCursor(Vector3 position)
    {
        cellIndicator.transform.position = position;
    }

    private void MovePreview(Vector3 position)
    {
        previewObject.transform.position = new Vector3(position.x, position.y + previewOffsetY, position.z);
    }

    public void StartShowingRemovePreview()
    {
        cellIndicator.SetActive(true);
        PrepareCursor(Vector2Int.one);
        ApplyFeebackToCursor(false);
    }

    public void UpdateRotation(Quaternion rotation)
    {
        if (previewObject != null && previewObject.transform.childCount > 0)
        {
            Transform child = previewObject.transform.GetChild(0); // Obtener el primer hijo
            child.rotation = rotation; // Rotar solo el hijo
        }
    }

}
