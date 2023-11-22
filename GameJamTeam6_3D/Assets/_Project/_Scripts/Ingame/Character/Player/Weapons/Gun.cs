using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Gun : MonoBehaviour, IWeapon
{
    [SerializeField] WeaponData data;
    float cooldown;
    float damage;
    //float roundBullets;

    bool canAttack = false;

    public void Setup()
    {
        cooldown = 0;
        damage = data.GetDamage();
    }

    public bool CanAttack()
    {
        cooldown -= Time.deltaTime;
        if (cooldown < 0)
        {
            canAttack = true;
        }
        else canAttack = false;

        return canAttack;
    }

    public void DoAttack()
    {
        cooldown = data.GetCoolDown();
        canAttack = false;
    }

    public int GetDamage()
    {
        //throw new System.NotImplementedException();
        return 10;
    }

    public GameObject GetObject()
    {
        //throw new System.NotImplementedException();
        return gameObject;
    }

}
