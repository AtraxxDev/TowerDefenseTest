using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ObjectDataBaseSO", menuName = "Scriptable Objects/ObjectDataBaseSO")]
public class ObjectDataBaseSO : ScriptableObject
{
    public List<ObjectData> objectData;
}

[Serializable]
public class ObjectData
{
    [field:SerializeField]
    public string Name { get; private set; }
    [field:SerializeField]
    public int ID { get; private set; }
    [field:SerializeField]
    public Vector2Int Size { get; private set; } = Vector2Int.one;
    [field:SerializeField]
    public GameObject prefab;

}
