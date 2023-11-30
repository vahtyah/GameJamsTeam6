using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "WeaponData", menuName = GlobalString.ItemData + "WeaponData")]
public class WeaponData : ScriptableObject, IItemEquipmentData
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
        
    }

    int id = -2;
    public int GetItemID()
    {
        return id;
    }

    public string GetItemName()
    {
        
    }

    public ItemType GetItemType()
    {
        
    }


    public void SetItemID(int _id)
    {
        id = _id;
    }

    public Sprite GetItemIcon()
    {
        return itemIcon;
    }

    public void OnEquip()
    {
        
    }

    public void OnUnEquip()
    {
        
    }
}
