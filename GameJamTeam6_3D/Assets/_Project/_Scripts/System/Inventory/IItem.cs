using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IItem 
{
    public void SetItemID(int _id);
    public int GetItemID();
    public string GetItemName();
    public string GetItemDescription();

    public ItemType GetItemType();

}

public enum ItemType
{
    Weapon, Armour, Gloves, Belt, Head, Shoes, Consumable, Material
}

public interface IWeaponItem : IItem
{
    public WeaponData GetWeaponData(); 
}




