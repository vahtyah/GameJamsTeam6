using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ArmorData", menuName = GlobalString.ItemData + "ArmorData")]
public class ArmorData : ScriptableObject, IItemEquipmentData
{
    [SerializeField] ItemType itemType;
    [SerializeField] Sprite itemIcon;
    [SerializeField] string itemName;
    [SerializeField] string description;
    public string GetItemDescription()
    {
       
    }

    public Sprite GetItemIcon()
    {
        return itemIcon;
    }

    public int GetItemID()
    {
        
    }

    public string GetItemName()
    {
        
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
        
    }
}
