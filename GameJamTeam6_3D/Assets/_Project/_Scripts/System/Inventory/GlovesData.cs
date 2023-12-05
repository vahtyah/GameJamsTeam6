using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GlovesData", menuName = GlobalString.ItemData + "GlovesData")]
public class GlovesData : ScriptableObject, IItemEquipmentData
{
    [SerializeField] int addCrit;

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
    ItemType type = ItemType.Gloves;
    public ItemType GetItemType()
    {
        return type;
    }

    public void OnEquip()
    {
        Player.instance.GetPlayerData().critChance.AddValue(addCrit);
    }

    public void OnUnEquip()
    {
        Player.instance.GetPlayerData().critChance.AddValue(-addCrit);
    }

    public void SetItemID(int _id)
    {
        id = _id;
    }
}
