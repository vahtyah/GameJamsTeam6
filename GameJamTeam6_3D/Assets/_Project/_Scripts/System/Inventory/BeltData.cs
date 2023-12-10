using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BeltData", menuName = GlobalInfo.ItemData + "BeltData")]
public class BeltData : ScriptableObject, IItemEquipmentData
{
    [SerializeField] int addCritDamgPercent;

    [SerializeField] Sprite itemIcon;
    [SerializeField] string itemName;
    [SerializeField] string description;

    public string GetItemDescription()
    {
        return StringBuilderSpecialist.SetAndGet(description + " + " + addCritDamgPercent.ToString());
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

    ItemType type = ItemType.Belt;
    public ItemType GetItemType()
    {
        return type;
    }

    public void OnEquip()
    {
        Player.instance.GetPlayerData().critDamgPercent.AddValue(addCritDamgPercent);
    }

    public void OnUnEquip()
    {
        Player.instance.GetPlayerData().critDamgPercent.AddValue(-addCritDamgPercent);
    }

    public void SetItemID(int _id)
    {
        id = _id;
    }

}
