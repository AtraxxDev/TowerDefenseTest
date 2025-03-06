using System.Collections;
using UnityEngine;

public interface IAttackable
{
    void Attack();
    IEnumerator AttackCooldown();

}
