using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamageCollision : MonoBehaviour
{
    [SerializeField] float prepareToAttackTimer = 0.5f;
    [SerializeField] float explodingTime = 0.5f;
    [SerializeField] ParticleSystem fx;

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
        if (fx != null) fx.Play();
        yield return new WaitForSeconds(explodingTime);
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
