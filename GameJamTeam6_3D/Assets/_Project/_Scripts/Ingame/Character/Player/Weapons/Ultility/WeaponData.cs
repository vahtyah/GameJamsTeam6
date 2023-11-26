using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "WeaponData", menuName = "WeaponData")]
public class WeaponData : ScriptableObject
{
    [SerializeField] int bulletID;
    [SerializeField] float cooldown;
    [SerializeField] int magazine;
    [SerializeField] int damage;

    public float GetCoolDown() => cooldown;
    public int GetMagazine()=> magazine;
    public int GetDamage() => damage;
    public int GetBulletID() => bulletID;



}
