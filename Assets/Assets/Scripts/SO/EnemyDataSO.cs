using UnityEngine;

[CreateAssetMenu(fileName = "EnemyDataSO", menuName = "Scriptable Objects/EnemyDataSO")]
public class EnemyDataSO : ScriptableObject
{
    public GameObject prefab;
    public int maxHealth;
    public float speed;
    public int damage;
    public float attackCooldown;
    public float attackRange;
}
