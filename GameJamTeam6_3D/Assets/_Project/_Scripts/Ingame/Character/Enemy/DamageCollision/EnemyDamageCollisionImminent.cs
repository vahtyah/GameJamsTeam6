using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamageCollisionImminent : MonoBehaviour
{
    [SerializeField] EnemyDamageCollision damageCollision;
    [SerializeField] float imminentTime = 2f;

    private void Awake()
    {
        damageCollision.complete = ()=> gameObject.SetActive(false);
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
