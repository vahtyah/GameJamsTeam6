using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HelmetData", menuName = GlobalInfo.ItemData + "HelmetData")]
public class HelmetData : ScriptableObject, IItemEquipmentData
{
    [SerializeField] int addMaxHp;

    [SerializeField] Sprite itemIcon;
    [SerializeField] string itemName;
    [SerializeField] string description;
    public string GetItemDescription()
    {
        return StringBuilderSpecialist.SetAndGet(description + " + " + addMaxHp.ToString());
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
    ItemType type = ItemType.Helmet;
    public ItemType GetItemType()
    {
        return type;
    }

    public void OnEquip()
    {
        Player.instance.GetPlayerData().maxHp.AddValue(addMaxHp);
        Player.instance.GetCharacterHealth().Setup(Player.instance.GetPlayerData().maxHp.value, Player.instance.GetCharacterHealth().CurHealth);
    }

    public void OnUnEquip()
    {
        Player.instance.GetPlayerData().maxHp.AddValue(-addMaxHp);
        Player.instance.GetCharacterHealth().Setup(Player.instance.GetPlayerData().maxHp.value, Player.instance.GetCharacterHealth().CurHealth);
    }

    public void SetItemID(int _id)
    {
        id = _id;
    }

}
