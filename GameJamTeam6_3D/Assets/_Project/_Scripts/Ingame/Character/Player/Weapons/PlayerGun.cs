using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using _Project._Scripts.Ingame.Manager;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerGun : MonoBehaviour, IWeapon
{
    const int shakeValue = 5;
    [SerializeField] WeaponData data;
    [SerializeField] Transform shootPos;
    [SerializeField] ParticleSystem muzzle;
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
        SoundManager.Instance.PlaySFX(Sound.Shot);
        canAttack = false;
        muzzle.Play();
        cooldown = data.GetCoolDown();
        var bullet = ProjectilePooling.instance.ActivateProjectile(data.GetBulletID()).SetDamage(DamageHelper.GetPlayerDamage(damage));
        bullet.SetPossession(_isPlayer: true);
        bullet.GetGameObject().transform.position = shootPos.position;
        bullet.GetGameObject().transform.LookAt(IngameManager.instance.mousePointer.position);
        bullet.GetGameObject().transform.eulerAngles = new Vector3(0, bullet.GetGameObject().transform.eulerAngles.y, 0);
        //bullet.GetGameObject().transform.Rotate(bullet.GetGameObject().transform.rotation.x
        //    , UnityEngine.Random.Range(bullet.GetGameObject().transform.rotation.y - shakeValue, bullet.GetGameObject().transform.rotation.y + shakeValue)
        //    , bullet.GetGameObject().transform.rotation.z
        //    );
    }

    public int GetDamage()
    {
        return damage;
    }

    public GameObject GetGameObject()
    {
        return gameObject;
    }

}
