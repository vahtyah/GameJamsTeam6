using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryPanelMouseTracker : MonoBehaviour, IEndDragHandler
{

    public void OnEndDrag(PointerEventData eventData)
    {
        if (DraggableEquipmentItem.instance.gameObject.activeSelf == false) return;
        if (eventData.pointerEnter == false) return;
        DraggableEquipmentItem.instance.Restore();
    }
}
