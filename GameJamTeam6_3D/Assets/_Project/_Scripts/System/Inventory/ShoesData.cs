using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "ShoesData", menuName = GlobalInfo.ItemData + "ShoesData")]
public class ShoesData : ScriptableObject, IItemEquipmentData
{
    [SerializeField] int addSpeed;

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
    ItemType type = ItemType.Shoes;
    public ItemType GetItemType()
    {
        return type;
    }

    public void OnEquip()
    {
        Player.instance.GetPlayerData().speed.AddValue(addSpeed);
    }

    public void OnUnEquip()
    {
        Player.instance.GetPlayerData().speed.AddValue(-addSpeed);
    }

    public void SetItemID(int _id)
    {
        id = _id;
    }

}
