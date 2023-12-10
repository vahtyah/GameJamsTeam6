using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ArmorData", menuName = GlobalInfo.ItemData + "ArmorData")]
public class ArmorData : ScriptableObject, IItemEquipmentData
{
    [SerializeField] int addDefValue;

    ItemType itemType = ItemType.Armour;
    [SerializeField] Sprite itemIcon;
    [SerializeField] string itemName;
    [SerializeField] string description;

    public string GetItemDescription()
    {
        return StringBuilderSpecialist.SetAndGet( description + " + " + addDefValue.ToString());
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
        Player.instance.GetPlayerData().def.AddValue(addDefValue);
    }

    public void OnUnEquip()
    {
        Player.instance.GetPlayerData().def.AddValue(-addDefValue);
    }

    public void SetItemID(int _id)
    {
        id = _id;
    }
}
