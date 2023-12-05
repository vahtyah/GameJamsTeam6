using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeleeDamageCollision : MonoBehaviour
{
    [SerializeField] float prepareToAttackTimer = 0.5f;
    
    int damage = 5;
    
    public void SetDamage(int _damage)
    {
        damage = _damage;
    }

    public void AttackOn()
    {
        StartCoroutine(IEAttack());
    }

    IEnumerator IEAttack()
    {
        yield return new WaitForSeconds(prepareToAttackTimer);
        gameObject.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(GlobalInfo.tagPlayer))
        {
            Player.instance.AddHealth(-damage);
        }
    }





}
