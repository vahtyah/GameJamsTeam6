using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerGun : MonoBehaviour, IWeapon
{
    const int shakeValue = 5;
    [SerializeField] WeaponData data;
    [SerializeField] Transform shootPos;
    [SerializeField] ParticleSystem muzzle;
    [SerializeField] GameObject muzzleLight;
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
        muzzle.Play();
        //muzzleLight.SetActive(true);
        cooldown = data.GetCoolDown();
        var bullet = ProjectilePooling.instance.ActivateProjectile(data.GetBulletID()).SetDamage(damage);
        bullet.SetPossession(_isPlayer: true);
        bullet.GetGameObject().transform.position = shootPos.position;
        bullet.GetGameObject().transform.rotation = Player.instance.GetModel().transform.rotation;
        bullet.GetGameObject().transform.Rotate(bullet.GetGameObject().transform.rotation.x
            , UnityEngine.Random.Range(bullet.GetGameObject().transform.rotation.y - shakeValue, bullet.GetGameObject().transform.rotation.y + shakeValue)
            , bullet.GetGameObject().transform.rotation.z
            );
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
