using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class InventorySystem : SerializedMonoBehaviour
{
    public static InventorySystem instance;

    public Action<ItemType> onEquip;
    /// <summary>
    /// 1st int : inventory Index,
    /// </summary>
    public Action<int> onAddedItemInventory;
    public Action<int> onRemoveItemInventory;

    [SerializeField] InventoryHolderData itemHolder;
    [Space][SerializeField] Dictionary<ItemType, IItemEquipmentData> itemEquipment;
    [Space][SerializeField] List<IItemData> itemInventory;
    InventoryEquipmentSaveLoadHandler inventoryEquipmentSaveLoadHandler = new InventoryEquipmentSaveLoadHandler();

    public Dictionary<ItemType, IItemEquipmentData> GetItemEquipment() { return itemEquipment; }
    public Sprite GetItemIcon(ItemType _type, int _id) => itemHolder.itemHolding[_type].data[_id].GetItemIcon();
    public List<IItemData> GetItemInventory() => itemInventory;
    public int GetItemInventoryID(int _inventoryIndex)
    {
        if (itemInventory[_inventoryIndex] == null) return -1;
        return itemInventory[_inventoryIndex].GetItemID();
    }
    public ItemType GetItemInventoryType(int _inventoryIndex)
    {
        if (itemInventory[_inventoryIndex] == null) return ItemType.Material;
        return itemInventory[_inventoryIndex].GetItemType();
    }
    public int GetItemEquipmentID(ItemType _type)
    {
        if (itemEquipment[_type] == null) return -1;
        return itemEquipment[_type].GetItemID();
    }

    public IItemData GetIItemFromHolder(ItemType _type, int _id) { return itemHolder.itemHolding[_type].data[_id]; }
    public bool HasEquipID(ItemType _type, int _id) { return _id >= 0 && _id < itemHolder.itemHolding[_type].data.Length; }


    private void Awake()
    {
        instance = this;
        onAddedItemInventory += AddInventory;
        foreach (var keyValue in itemHolder.itemHolding)
        {
            for (int i = 0; i < keyValue.Value.data.Length; i++)
            {
                keyValue.Value.data[i].SetItemID(i);
            }
        }
        inventoryEquipmentSaveLoadHandler.Load();
    }


    void AddInventory(int _inventoryIndexSlot)
    {
        StartCoroutine(inventoryEquipmentSaveLoadHandler.IESaveInventory(_inventoryIndexSlot));
    }

    public void RemoveInventory(int _inventoryIndexSlot)
    {
        itemInventory[_inventoryIndexSlot] = null;
        onRemoveItemInventory?.Invoke(_inventoryIndexSlot);
        StartCoroutine(inventoryEquipmentSaveLoadHandler.IESaveInventoryRemoved(_inventoryIndexSlot));
    }

    public void InitInventory(List<IItemData> _items)
    {
        itemInventory = _items;
    }

    public void InitEquipment(Dictionary<ItemType, IItemEquipmentData> _equipments)
    {
        itemEquipment = _equipments;
    }

    public void InitPlayerEquip()
    {
        foreach (var value in itemEquipment.Values)
        {
            if (value == null) continue;
            value.OnEquip();
        }
    }

    IItemData tempItemData;
    public void SwapEquipmentInventoryItem(int _inventoryIndexSlot, ItemType _equipmentType)
    {
        tempItemData = itemInventory[_inventoryIndexSlot];
        //if (itemEquipment[_equipmentType] != null)
        //{
            itemInventory[_inventoryIndexSlot] = itemEquipment[_equipmentType];
            onAddedItemInventory?.Invoke(_inventoryIndexSlot);
        //}
        if (itemEquipment[_equipmentType] != null) itemEquipment[_equipmentType].OnUnEquip();
        itemEquipment[_equipmentType] = tempItemData as IItemEquipmentData;
        if (itemEquipment[_equipmentType] != null) itemEquipment[_equipmentType].OnEquip();
        inventoryEquipmentSaveLoadHandler.SaveEquipment();
        onEquip?.Invoke(_equipmentType);
    }

    public void SwapInventoryItems(int _atInventoryIndex, int _toInventoryIndex)
    {
        tempItemData = itemInventory[_toInventoryIndex];
        itemInventory[_toInventoryIndex] = itemInventory[_atInventoryIndex];
        onAddedItemInventory?.Invoke(_toInventoryIndex);
        itemInventory[_atInventoryIndex] = tempItemData;
        onAddedItemInventory?.Invoke(_atInventoryIndex);
    }

    public void AddItemToInventory(ItemType _type, int _id)
    {
        for (int i = 0; i < itemInventory.Count; i++)
        {
            if (itemInventory[i] == null)
            {
                itemInventory[i] = itemHolder.itemHolding[_type].data[_id];
                onAddedItemInventory?.Invoke(i);
                return;
            }
        }
    }

    public void AddItemToInventory(int _inventoryIndexSlot, ItemType _type, int _id)
    {
        itemInventory[_inventoryIndexSlot] = itemHolder.itemHolding[_type].data[_id];
        onAddedItemInventory?.Invoke(_inventoryIndexSlot);
    }

}

public class InventoryEquipmentSaveLoadHandler
{
    const string inventorySaveStringTxt = "/inventoryItem.txt";
    const string equipmentSaveStringTxt = "/equipmentItem.txt";
    string inventorySaveString;
    string equipmentSaveString;
    /// <summary>
    /// int : id
    /// </summary>
    Dictionary<ItemType, int> equipmentSaveLoad = new Dictionary<ItemType, int>();
    /// <summary>
    /// int : int
    /// </summary>
    List<Tuple<ItemType, int>> inventorySaveLoad = new List<Tuple<ItemType, int>>();

    public void Load()
    {
        inventorySaveString = Application.persistentDataPath + inventorySaveStringTxt;
        equipmentSaveString = Application.persistentDataPath + equipmentSaveStringTxt;
        LoadEquipment();
        LoadInventory();
    }

    void LoadEquipment()
    {
        if (IOSystemic.CheckFileExist(equipmentSaveString) == false) return;
        Dictionary<ItemType, IItemEquipmentData> equipments = new Dictionary<ItemType, IItemEquipmentData>();
        equipmentSaveLoad = IOSystemic.LoadData<Dictionary<ItemType, int>>(equipmentSaveString);
        foreach (var keyValue in equipmentSaveLoad)
        {
            equipments[keyValue.Key] = InventorySystem.instance.GetIItemFromHolder(keyValue.Key, keyValue.Value) as IItemEquipmentData;
        }
        InventorySystem.instance.InitEquipment(equipments);
    }

    public void SaveEquipment()
    {
        foreach (var keyValue in InventorySystem.instance.GetItemEquipment())
        {
            if (keyValue.Value == null) continue;
            equipmentSaveLoad[keyValue.Key] = keyValue.Value.GetItemID();
        }
        IOSystemic.SaveData<Dictionary<ItemType, int>>(equipmentSaveLoad, equipmentSaveString);
    }

    void LoadInventory()
    {
        if (IOSystemic.CheckFileExist(inventorySaveString) == false)
        {
            for (int i = 0; i < InventorySystem.instance.GetItemInventory().Count; i++)
            {
                inventorySaveLoad.Add(null);
            }
            return;
        }
        List<IItemData> inventory = new List<IItemData>();
        inventorySaveLoad = IOSystemic.LoadData<List<Tuple<ItemType, int>>>(inventorySaveString);
        for (int i = 0; i < inventorySaveLoad.Count; i++)
        {
            if (inventorySaveLoad[i] == null)
            {
                inventory.Add(null);
                continue;
            }
            inventory.Add(InventorySystem.instance.GetIItemFromHolder(inventorySaveLoad[i].Item1, inventorySaveLoad[i].Item2));
        }
        InventorySystem.instance.InitInventory(inventory);
    }

    public IEnumerator IESaveInventory()
    {
        for (int i = 0; i < InventorySystem.instance.GetItemInventory().Count; i++)
        {
            if (InventorySystem.instance.GetItemInventory()[i] == null) continue;
            inventorySaveLoad[i] = (InventorySystem.instance.GetItemInventory()[i].GetItemType()
                , InventorySystem.instance.GetItemInventory()[i].GetItemID()).ToTuple();
        }
        IOSystemic.SaveData(inventorySaveLoad, inventorySaveString);
        yield return null;
    }
    public IEnumerator IESaveInventory(int _indexInventory)
    {
        if (InventorySystem.instance.GetItemInventory()[_indexInventory] == null) yield break;
        inventorySaveLoad[_indexInventory] = (InventorySystem.instance.GetItemInventory()[_indexInventory].GetItemType()
            , InventorySystem.instance.GetItemInventory()[_indexInventory].GetItemID()).ToTuple();
        IOSystemic.SaveData(inventorySaveLoad, inventorySaveString);
        yield return null;
    }
    public IEnumerator IESaveInventoryRemoved(int _indexInventory)
    {
        inventorySaveLoad[_indexInventory] = null;
        IOSystemic.SaveData(inventorySaveLoad, inventorySaveString);
        yield return null;
    }

}

public class ItemInfoData
{
    public ItemType itemType;
    public int id;
}
[Serializable]
public class ListIItemData
{
    [ListDrawerSettings(ShowIndexLabels = true)]
    public IItemData[] data;
}