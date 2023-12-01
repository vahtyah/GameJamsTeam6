using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ArmorData", menuName = GlobalString.ItemData + "ArmorData")]
public class ArmorData : ScriptableObject, IItemEquipmentData
{
    [SerializeField] int defValue;

    [SerializeField] ItemType itemType;
    [SerializeField] Sprite itemIcon;
    [SerializeField] string itemName;
    [SerializeField] string description;

    public string GetItemDescription()
    {
        return description;
    }

    public Sprite GetItemIcon()
    {
        return itemIcon;
    }
    int id = -1;
    public int GetItemID()
    {
        return id;
    }

    public string GetItemName()
    {
        return itemName;
    }

    public ItemType GetItemType()
    {
        return itemType;
    }

    public void OnEquip()
    {
        
    }

    public void OnUnEquip()
    {
        
    }

    public void SetItemID(int _id)
    {
        id = _id;
    }
}
