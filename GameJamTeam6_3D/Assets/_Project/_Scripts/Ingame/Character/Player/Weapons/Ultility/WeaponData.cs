using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "WeaponData", menuName = "WeaponData")]
public class WeaponData : ScriptableObject, IItem
{
    [SerializeField] Sprite itemIcon;
    [SerializeField] string itemName;
    [SerializeField] string description;
    [SerializeField] int bulletID;
    [SerializeField] float cooldown;
    [SerializeField] int magazine;
    [SerializeField] int damage;

    public float GetCoolDown() => cooldown;
    public int GetMagazine()=> magazine;
    public int GetDamage() => damage;
    public int GetBulletID() => bulletID;

    public string GetItemDescription()
    {
        throw new System.NotImplementedException();
    }

    public int GetItemID()
    {
        throw new System.NotImplementedException();
    }

    public string GetItemName()
    {
        throw new System.NotImplementedException();
    }

    public ItemType GetItemType()
    {
        throw new System.NotImplementedException();
    }

    public void SetItemID(int _id)
    {
        throw new System.NotImplementedException();
    }

    public Sprite GetItemIcon()
    {
        throw new System.NotImplementedException();
    }
}
