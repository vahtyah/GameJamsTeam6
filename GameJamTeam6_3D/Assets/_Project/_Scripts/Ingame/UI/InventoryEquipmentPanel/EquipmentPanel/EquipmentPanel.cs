using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentPanel : SerializedMonoBehaviour
{
    [SerializeField] Dictionary<ItemType, IItem> equippedItems = new Dictionary<ItemType, IItem>() {
        {ItemType.Belt, null },
        {ItemType.Helmet, null},
        {ItemType.Weapon, null },
        {ItemType.Shoes, null },
        {ItemType.Gloves, null},
        {ItemType.Armour, null },
    };

    private void Start()
    {
        InventorySystem.instance.onEquip = Equip;
        
    }

    public void Equip(ItemType _type, int id)
    {
        equippedItems[_type] = InventorySystem.instance.GetIItem(_type, id);
    }


}