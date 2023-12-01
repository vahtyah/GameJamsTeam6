using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GlovesData", menuName = GlobalString.ItemData + "GlovesData")]
public class GlovesData : ScriptableObject, IItemEquipmentData
{
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
    [SerializeField] ItemType type;
    public ItemType GetItemType()
    {
        return type;
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
