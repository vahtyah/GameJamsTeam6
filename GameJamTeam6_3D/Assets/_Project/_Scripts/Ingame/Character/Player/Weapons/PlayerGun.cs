using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerGun : MonoBehaviour, IWeapon
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
        if (cooldown < 0 && InputHandler.instance.IsNormalAttackHoldDown())
        {
            canAttack = true;
        }
        else canAttack = false;
        return canAttack;
    }

    public void Shoot()
    {
        canAttack = false;
        cooldown = data.GetCoolDown();
        var bullet = ProjectilePooling.instance.ActivateProjectile(data.GetBulletID()).SetDamage(damage);
        bullet.SetPossession(_isPlayer: true);
        bullet.GetGameObject().transform.position = shootPos.position;
        bullet.GetGameObject().transform.rotation = Player.instance.GetModel().transform.rotation;
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
