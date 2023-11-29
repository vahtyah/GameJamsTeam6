using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySystem : SerializedMonoBehaviour
{
    public static InventorySystem instance;

    public Action<ItemType, int> onEquip;
    /// <summary>
    /// 1st int : inventory Index,
    /// 2nd int : itemType Index
    /// </summary>
    public Action<int, ItemType, int> onAddedItemInventory;
    public Action<int> onRemoveItemInventory;

    [SerializeField] Dictionary<ItemType, List<IItem>> itemHolders;
    [Space][SerializeField] Dictionary<ItemType, IItem> itemEquipment;
    [Space][SerializeField] List<IItem> itemInventory;

    public Dictionary<ItemType, List<IItem>> GetItemHolders() { return itemHolders; }
    public Sprite GetItemIcon(ItemType _type, int _id) => itemHolders[_type][_id].GetItemIcon();
    public List<IItem> GetItemInventory() => itemInventory;
    public IItem GetIItem(ItemType _type, int _id)
    {
        return itemHolders[_type][_id];
    }

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

    public void SwapItemEquipmentInventory(int _inventoryIndexSlot, ItemType _type, int _id)
    {
        if (itemEquipment[_type] != null)
        {
            itemInventory[_inventoryIndexSlot] = itemEquipment[_type];
            onAddedItemInventory?.Invoke(_inventoryIndexSlot, _type, _id);
        }
        itemEquipment[_type] = itemHolders[_type][_id];
        onEquip?.Invoke(_type, _id);
    }

    public void SwapItemInventory(int _swappingInventoryIndexSlot, int _targetInventoryIndex, ItemType _type, int _id)
    {
        if (itemInventory[_targetInventoryIndex] != null)
        {
            itemInventory[_swappingInventoryIndexSlot] = itemInventory[_targetInventoryIndex];
            onAddedItemInventory?.Invoke(_swappingInventoryIndexSlot, _type, _id);
        }
        itemInventory[_targetInventoryIndex] = itemHolders[_type][_id];
        onAddedItemInventory?.Invoke(_targetInventoryIndex, _type, _id);
    }

    public void AddItemToInventory(ItemType _type, int _id)
    {
        for (int i = 0; i < itemInventory.Count; i++)
        {
            if (itemInventory[i] == null)
            {
                itemInventory[i] = itemHolders[_type][_id];
                onAddedItemInventory?.Invoke(i, _type, _id);
                return;
            }
        }
    }

    public void AddItemToInventory(int _inventoryIndexSlot, ItemType _type, int _id)
    {
        itemInventory[_inventoryIndexSlot] = itemHolders[_type][_id];
        onAddedItemInventory?.Invoke(_inventoryIndexSlot, _type, _id);
    }

}

