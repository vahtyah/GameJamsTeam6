using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySystem : SerializedMonoBehaviour
{
    public static InventorySystem instance;

    [SerializeField] Dictionary<ItemType, List<IItem>> itemHolders;
    [Space][SerializeField] Dictionary<ItemType, IItem> itemEquipment;
    [Space][SerializeField] List<IItem> itemInventory;

    private void Awake()
    {
        instance = this;
        foreach (var keyValue in itemHolders)
        {
            for (int i = 0; i < keyValue.Value.Count; i++)
            {
                keyValue.Value[i].SetItemID(i);
            }
        }
    }

    public IItem GetItemInfo(ItemType _type, int _id)
    {
        return itemHolders[_type][_id];
    }

    public void SwapItem(int _inventoryIndexSlot, ItemType _type, int _id)
    {
        if (itemEquipment[_type] != null)
        {
            itemInventory[_inventoryIndexSlot] = itemEquipment[_type];
        }
        itemEquipment[_type] = itemHolders[_type][_id];
    }

    public void AddItemToInventory(ItemType _type, int _id)
    {
        for (int i = 0; i < itemInventory.Count; i++)
        {
            if (itemInventory[i] == null)
            {
                itemInventory[i] = itemHolders[_type][_id];
                return;
            }
        }


    }



}

