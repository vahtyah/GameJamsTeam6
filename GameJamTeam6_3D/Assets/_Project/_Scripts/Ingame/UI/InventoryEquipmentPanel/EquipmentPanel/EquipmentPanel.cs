using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentPanel : SerializedMonoBehaviour
{
    [SerializeField]
    Dictionary<ItemType, EquipmentItemUI> equippedItems = new Dictionary<ItemType, EquipmentItemUI>() {
        {ItemType.Belt, null },
        {ItemType.Helmet, null},
        {ItemType.Weapon, null },
        {ItemType.Shoes, null },
        {ItemType.Gloves, null},
        {ItemType.Armour, null },
    };

    private void Start()
    {
        InventorySystem.instance.onEquip += Equip;
        foreach (var keyValue in InventorySystem.instance.GetItemEquipment())
        {
            equippedItems[keyValue.Key].SetData(keyValue.Key);
        }
    }

    private void OnDestroy()
    {
        InventorySystem.instance.onEquip -= Equip;
    }

    public void Equip(ItemType _type)
    {

        equippedItems[_type].SetData(_type);

    }


}
