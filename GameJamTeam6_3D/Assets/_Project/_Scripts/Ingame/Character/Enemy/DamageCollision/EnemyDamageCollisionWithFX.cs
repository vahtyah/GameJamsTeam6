using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamageCollisionWithFX : MonoBehaviour
{
    [SerializeField] float prepareToAttackTimer = 0.5f;
    [SerializeField] float duration = 0.5f;
    [SerializeField] ParticleSystem fx;


    int damage = 5;

    public void SetDamage(int _damage)
    {
        damage = _damage;
    }

    public void AttackOn()
    {
        gameObject.SetActive(true);
        if (fx != null) fx.Play();
        StartCoroutine(IEEndAttack());
    }

    IEnumerator IEEndAttack()
    {
        yield return new WaitForSeconds(duration);
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
