using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HelmetData", menuName = GlobalString.ItemData + "HelmetData")]
public class HelmetData : ScriptableObject, IItemEquipmentData
{
    [SerializeField] int addMaxHp;

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
        Player.instance.GetPlayerData().maxHp.AddValue(addMaxHp);
    }

    public void OnUnEquip()
    {
        Player.instance.GetPlayerData().maxHp.AddValue(-addMaxHp);
    }

    public void SetItemID(int _id)
    {
        id = _id;
    }

}
