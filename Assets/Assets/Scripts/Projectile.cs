using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Transform target;
    private int damage;
    private float speed = 10f;

    public void SetTarget(Transform newTarget, int _damage)
    {
        target = newTarget;
        damage = _damage;


    }


    private void Update()
    {
        if (target == null || !target.gameObject.activeInHierarchy)
        {
            Destroy(gameObject);
            return;
        }
        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform == target)
        {
            HitTarget();
        }

    }

    private void HitTarget()
    {
        if (target.TryGetComponent<IDamagable>(out IDamagable enemy))
        {
            enemy.TakeDamage(damage);
        }
        Destroy(gameObject);
    }
}
