using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
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

    [SerializeField] Dictionary<ItemType, List<IItemData>> itemHolders;
    [Space][SerializeField] Dictionary<ItemType, IItemEquipmentData> itemEquipment;
    [Space][SerializeField] List<IItemData> itemInventory;
    InventoryEquipmentSaveLoadHandler inventoryEquipmentSaveLoadHandler = new InventoryEquipmentSaveLoadHandler();

    public Dictionary<ItemType, IItemEquipmentData> GetItemEquipment() { return itemEquipment; }
    public Dictionary<ItemType, List<IItemData>> GetItemHolders() { return itemHolders; }
    public Sprite GetItemIcon(ItemType _type, int _id) => itemHolders[_type][_id].GetItemIcon();
    public List<IItemData> GetItemInventory() => itemInventory;
    public IItemData GetIItemFromHolder(ItemType _type, int _id)
    {
        return itemHolders[_type][_id];
    }

    private void Awake()
    {
        instance = this;
        onEquip += Equip;
        onAddedItemInventory += AddInventory;
        onRemoveItemInventory += RemoveInventory;
        foreach (var keyValue in itemHolders)
        {
            for (int i = 0; i < keyValue.Value.Count; i++)
            {
                keyValue.Value[i].SetItemID(i);
            }
        }
        inventoryEquipmentSaveLoadHandler.Load();
    }

    void Equip(ItemType _type, int _id)
    {
        if (itemEquipment[_type] != null) itemEquipment[_type].OnUnEquip();
        itemEquipment[_type] = itemHolders[_type][_id] as IItemEquipmentData;
        itemEquipment[_type].OnEquip();
        inventoryEquipmentSaveLoadHandler.SaveEquipment();
    }

    void AddInventory(int _inventoryIndexSlot, ItemType _type, int _id)
    {
        inventoryEquipmentSaveLoadHandler.SaveInventory();
    }

    void RemoveInventory(int _inventoryIndexSlot)
    {
        inventoryEquipmentSaveLoadHandler.SaveInventory();
    }

    public void InitInventory(List<IItemData> _items)
    {
        itemInventory = _items;
    }

    public void InitEquipment(Dictionary<ItemType, IItemEquipmentData> _equipments)
    {
        itemEquipment = _equipments;
        foreach (var value in itemEquipment.Values)
        {
            value.OnEquip();
        }
    }

    public void SwapItemEquipmentInventory(int _inventoryIndexSlot, ItemType _type, int _id)
    {
        if (itemEquipment[_type] != null)
        {
            itemInventory[_inventoryIndexSlot] = itemEquipment[_type];
            onAddedItemInventory?.Invoke(_inventoryIndexSlot, _type, _id);
        }
        onEquip?.Invoke(_type, _id);
    }

    public void SwapItemInventory(int _swappingInventoryIndexSlot, int _targetInventoryIndex, ItemType _type, int _id)
    {
        if (itemInventory[_targetInventoryIndex] != null)
        {
            itemInventory[_swappingInventoryIndexSlot] = itemInventory[_targetInventoryIndex];
            onAddedItemInventory?.Invoke(_swappingInventoryIndexSlot, itemInventory[_swappingInventoryIndexSlot].GetItemType()
                , itemInventory[_swappingInventoryIndexSlot].GetItemID());
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
            equipmentSaveLoad[keyValue.Key] = keyValue.Value.GetItemID();
        }
        IOSystemic.SaveData<Dictionary<ItemType, int>>(equipmentSaveLoad, equipmentSaveString);
    }

    void LoadInventory()
    {
        if (IOSystemic.CheckFileExist(inventorySaveString) == false) return;
        List<IItemData> inventory = new List<IItemData>();
        inventorySaveLoad = IOSystemic.LoadData<List<Tuple<ItemType, int>>>(inventorySaveString);
        for (int i = 0; i < inventorySaveLoad.Count; i++)
        {
            inventory.Add(InventorySystem.instance.GetIItemFromHolder(inventorySaveLoad[i].Item1, inventorySaveLoad[i].Item2));
        }
        InventorySystem.instance.InitInventory(inventory);
    }

    public void SaveInventory()
    {
        for (int i = 0; i < InventorySystem.instance.GetItemInventory().Count; i++)
        {
            inventorySaveLoad[i] = (InventorySystem.instance.GetItemInventory()[i].GetItemType()
                , InventorySystem.instance.GetItemInventory()[i].GetItemID()).ToTuple();
        }
        IOSystemic.SaveData(inventorySaveLoad, inventorySaveString);
    }

}

public class ItemInfoData
{
    public ItemType itemType;
    public int id;
}

