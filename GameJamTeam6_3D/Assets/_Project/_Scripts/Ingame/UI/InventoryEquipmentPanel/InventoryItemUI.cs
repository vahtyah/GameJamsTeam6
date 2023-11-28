using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryItemUI : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
    , IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] Image itemIcon;

    int itemID;
    ItemType type;
    int inventoryIndex;

    bool mouseEnter;
    bool isClick = false;

    Vector2 mousePosition;

    public void OnDrag(PointerEventData eventData)
    {
        DraggableEquipmentItem.instance.SetVisual(type, itemID, _fromEquipment: false, inventoryIndex);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isClick = true;
        mousePosition = Input.mousePosition;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        mouseEnter = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        mouseEnter = false;
        isClick = false;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Vector2 resultVector = (mousePosition - new Vector2(Input.mousePosition.x, Input.mousePosition.y));
        if (isClick && resultVector.magnitude < 0.5f)
        {
            //show infopanel
            return;
        }

        if (DraggableEquipmentItem.instance.FromEquipment)
        {
            InventorySystem.instance.SwapItemEquipmentInventory(inventoryIndex
                , DraggableEquipmentItem.instance.Type
                , DraggableEquipmentItem.instance.ItemID);
        }
        else
        {
            InventorySystem.instance.SwapItemInventory(DraggableEquipmentItem.instance.InventoryIndex
                , inventoryIndex
                , DraggableEquipmentItem.instance.Type
                , DraggableEquipmentItem.instance.ItemID
                );

        }
        type = DraggableEquipmentItem.instance.Type;
        itemID = DraggableEquipmentItem.instance.ItemID;
        SetItemIcon();
    }



    public void SetData(EquipmentDataCellUI _data)
    {
        inventoryIndex = _data.inventoryIndex;
        itemID = _data.itemID;
        type = _data.itemType;
        SetItemIcon();
    }

    void SetItemIcon()
    {
        itemIcon.sprite = InventorySystem.instance.GetItemIcon(type, itemID);
    }



}
