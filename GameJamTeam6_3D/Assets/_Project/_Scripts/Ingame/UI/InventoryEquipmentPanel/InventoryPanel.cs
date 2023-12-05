using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnhancedUI.EnhancedScroller;
using System;
using Sirenix.OdinInspector;

public class InventoryPanel : SerializedMonoBehaviour, IEnhancedScrollerDelegate
{
    public const int totalItemInRow = 4;
    [SerializeField] InventoryItemRowUI prefab;
    [Space][SerializeField] GameObject activePanel;
    public void SetActivePanel(bool _check) { activePanel.SetActive(_check); }
    public GameObject GetActivePanel() { return activePanel; }  
    [SerializeField] EnhancedScroller scroller;
    [Header("Debug")]
    [SerializeField] List<EquipmentDataRowUI> data = new List<EquipmentDataRowUI>();


    private void Awake()
    {
        scroller.Delegate = this;
    }

    void Start()
    {
        InventorySystem.instance.onAddedItemInventory += OnAddingItem;
        activePanel.SetActive(false);
    }

    void OnEnable()
    {
        SetData();
    }

    void OnDestroy()
    {
        InventorySystem.instance.onAddedItemInventory -= OnAddingItem;
    }

    void OnAddingItem(int _inventoryIndex)
    {
        int rowIndex = _inventoryIndex % totalItemInRow;
        data[Mathf.FloorToInt(_inventoryIndex / totalItemInRow)].rows[rowIndex].itemID = InventorySystem.instance.GetItemInventory()[_inventoryIndex] != null ?
            InventorySystem.instance.GetItemInventory()[_inventoryIndex].GetItemID() 
            : -1;
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
            if (InventorySystem.instance.GetItemInventory()[i] == null)
            {
                temp.rows[countItemInRow].itemID = -1;
            }
            else
            {
                temp.rows[countItemInRow].itemID = InventorySystem.instance.GetItemInventory()[i].GetItemID();
                temp.rows[countItemInRow].itemType = InventorySystem.instance.GetItemInventory()[i].GetItemType();
            }
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
[Serializable]
public class EquipmentDataRowUI
{
    public EquipmentDataCellUI[] rows = new EquipmentDataCellUI[InventoryPanel.totalItemInRow];
    public EquipmentDataRowUI()
    {
        for (int i = 0; i < rows.Length; i++)
        {
            rows[i] = new EquipmentDataCellUI();
        }
    }
}

public class EquipmentDataCellUI
{

    public int inventoryIndex;
    public ItemType itemType;
    public int itemID;
}

