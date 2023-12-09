using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamageCollisionImminent : MonoBehaviour
{
    public Action complete;
    [SerializeField] EnemyDamageCollision damageCollision;
    [SerializeField] float imminentTime = 2f;
    [SerializeField] float collisionPrepareToAttackTimer = 0.2f;
    [SerializeField] float collidingDuration = 0.5f;

    private void Awake()
    {
        damageCollision.complete = () =>
        {
            complete?.Invoke();
            gameObject.SetActive(false);
        };

    }

    public void SetDamage(int _damage)
    {
        damageCollision.SetDamage( _damage );
    }

    public void StartComing()
    {
        gameObject.SetActive(true);
        StartCoroutine(IEStartComing());
    }

    IEnumerator IEStartComing()
    {
        yield return new WaitForSeconds(imminentTime);
        damageCollision.AttackOn();
    }
}
