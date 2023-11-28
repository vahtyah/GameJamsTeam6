using EnhancedUI.EnhancedScroller;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItemRowUI : EnhancedScrollerCellView
{
    [SerializeField] InventoryItemUI[] items;

    public void SetData(EquipmentDataCellUI[] _rowData)
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (i >= _rowData.Length)
            {
                items[i].gameObject.SetActive(false);
                continue;
            }
            items[i].gameObject.SetActive(true);
            items[i].SetData(_rowData[i]);
        }
    }


}
