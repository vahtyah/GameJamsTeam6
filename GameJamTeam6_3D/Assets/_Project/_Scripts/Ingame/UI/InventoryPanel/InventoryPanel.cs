using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnhancedUI.EnhancedScroller;

public class InventoryPanel : MonoBehaviour, IEnhancedScrollerDelegate
{
    EnhancedScroller scroller;
    [SerializeField] List<EquipmentDataRowUI> data;

    private void Awake()
    {
        scroller.Delegate = this;
    }

    void SetData()
    {

    }

    public EnhancedScrollerCellView GetCellView(EnhancedScroller scroller, int dataIndex, int cellIndex)
    {
        throw new System.NotImplementedException();
    }

    public float GetCellViewSize(EnhancedScroller scroller, int dataIndex)
    {
        throw new System.NotImplementedException();
    }

    public int GetNumberOfCells(EnhancedScroller scroller)
    {
        throw new System.NotImplementedException();
    }
}

public class EquipmentDataRowUI
{
    public EquipmentDataUI[] rows = new EquipmentDataUI[4];
}

public class EquipmentDataUI
{
    public ItemType itemType;
    public int id;
}

