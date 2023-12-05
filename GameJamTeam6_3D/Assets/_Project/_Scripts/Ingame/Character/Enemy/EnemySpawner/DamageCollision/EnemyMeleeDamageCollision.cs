using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeleeDamageCollision : MonoBehaviour
{
    int damage = 5;
    
    public void SetDamage(int _damage)
    {
        damage = _damage;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(GlobalString.tagPlayer))
        {
            Player.instance.AddHealth(damage);
        }
    }





}
