using UnityEngine;

[CreateAssetMenu(fileName = "TowerDataSO", menuName = "Scriptable Objects/TowerDataSO")]
public class TowerDataSO : ScriptableObject
{
    public GameObject prefab;
    public int damage;
    public float attackCooldown;
    public float attackRange;
    public float detectionRange;
}
