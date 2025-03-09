using System;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPlacer : MonoBehaviour
{
    [SerializeField] private List<GameObject> placedGameObjects = new();

    public int PlaceObject(GameObject prefab, Vector3 position, Quaternion rotation)
    {
        GameObject newObject = Instantiate(prefab);
        newObject.transform.position = position;

        if (newObject.transform.childCount > 0)
        {
            Transform child = newObject.transform.GetChild(0);
            child.rotation = rotation;
        }

        placedGameObjects.Add(newObject);
        return placedGameObjects.Count - 1;
    }


    internal void RemoveObjectAt(int gameObjectIndex)
    {
        if (placedGameObjects.Count <= gameObjectIndex || placedGameObjects[gameObjectIndex] == null)
            return;
        Destroy(placedGameObjects[gameObjectIndex]);
        placedGameObjects[gameObjectIndex] = null;
       
    }
}
