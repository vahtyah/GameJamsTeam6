using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Gun : MonoBehaviour, IWeapon
{
    [SerializeField] WeaponData data;
    [SerializeField] Transform shootPos;
    float cooldown;
    int damage;
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

    public void Shoot()
    {
        cooldown = data.GetCoolDown();
        ProjectilePooling.instance.ActivateProjectile(data.GetBulletID()).SetDamage(damage).SetPosition(shootPos.position);
        canAttack = false;
    }

    public int GetDamage()
    {
        return damage;
    }

    public GameObject GetObject()
    {
        return gameObject;
    }

}
