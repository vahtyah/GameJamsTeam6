using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamageCollisionImminent : MonoBehaviour
{
    [SerializeField] EnemyDamageCollision damageCollision;
    [SerializeField] float imminentTime = 2f;
    //float timer;

    public void StartComing()
    {
        StartCoroutine(IEStartComing());
    }

    IEnumerator IEStartComing()
    {
        yield return new WaitForSeconds(imminentTime);
        damageCollision.AttackOn();
    }
}
