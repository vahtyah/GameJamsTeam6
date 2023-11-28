using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnhancedUI.EnhancedScroller;

public class InventoryPanel : MonoBehaviour, IEnhancedScrollerDelegate
{
    public const int totalItemInRow = 4;
    [SerializeField] InventoryItemRowUI prefab;
    [Space][SerializeField] GameObject activePanel;
    [SerializeField] EnhancedScroller scroller;
    List<EquipmentDataRowUI> data = new List<EquipmentDataRowUI>();


    private void Awake()
    {
        scroller.Delegate = this;
    }

    void Start()
    {
        InventorySystem.instance.onAddedItemInventory = OnAddingItem;
        activePanel.SetActive(false);
        SetData();
    }

    void OnAddingItem(int _inventoryIndex, ItemType _type, int _id)
    {
        int rowIndex = _inventoryIndex % totalItemInRow;
        data[Mathf.FloorToInt(_inventoryIndex / totalItemInRow)].rows[rowIndex].itemID = _id;
    }

    void SetData()
    {
        data.Clear();
        int countItemInRow = 0;
        EquipmentDataRowUI temp = null;
        for (int i = 0; i < InventorySystem.instance.GetItemInventory().Count; i++)
        {
            if (countItemInRow == 0)
            {
                temp = null;
                temp = new EquipmentDataRowUI();
                data.Add(temp);
            }
            temp.rows[countItemInRow].inventoryIndex = i;
            temp.rows[countItemInRow].itemID = InventorySystem.instance.GetItemInventory()[i].GetItemID();
            temp.rows[countItemInRow].itemType = InventorySystem.instance.GetItemInventory()[i].GetItemType();
            countItemInRow++;
            if (countItemInRow == totalItemInRow) countItemInRow = 0;
        }
    }

    InventoryItemRowUI tempRow;
    public EnhancedScrollerCellView GetCellView(EnhancedScroller scroller, int dataIndex, int cellIndex)
    {
        tempRow = scroller.GetCellView(prefab) as InventoryItemRowUI;
        tempRow.SetData(data[dataIndex].rows);
        return tempRow;
    }

    public float GetCellViewSize(EnhancedScroller scroller, int dataIndex)
    {
        return (prefab.transform as RectTransform).sizeDelta.y;
    }

    public int GetNumberOfCells(EnhancedScroller scroller)
    {
        return data.Count;
    }
}

public class EquipmentDataRowUI
{
    public EquipmentDataCellUI[] rows = new EquipmentDataCellUI[InventoryPanel.totalItemInRow];
}

public class EquipmentDataCellUI
{
    public int inventoryIndex;
    public ItemType itemType;
    public int itemID;
}

