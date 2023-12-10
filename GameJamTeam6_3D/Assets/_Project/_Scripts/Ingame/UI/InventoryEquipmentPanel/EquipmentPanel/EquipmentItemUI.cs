using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EquipmentItemUI : MonoBehaviour, IPointerDownHandler, IDragHandler, IEndDragHandler
    , IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] GameObject equipmentTypeIdleActive;
    [SerializeField] GameObject active;
    [SerializeField] Image itemIcon;
    [SerializeField] ItemType type;
    [Header("Debug")]
    public int itemID = -1;

    [SerializeField] bool mouseEnter;
    bool isClick = false;
    Vector2 mousePosition;

    void Start()
    {
        DraggableEquipmentItem.instance.onDrop += OnDrop;
    }

    void OnDrop()
    {
        if (mouseEnter == false) return;
        if (DraggableEquipmentItem.instance.gameObject.activeSelf == false) return;
        if (DraggableEquipmentItem.instance.FromEquipment) return;
        if (DraggableEquipmentItem.instance.Type != type)
        {
            DraggableEquipmentItem.instance.Restore();
            return;
        }
        InventorySystem.instance.SwapEquipmentInventoryItem(DraggableEquipmentItem.instance.InventoryIndex, type);
        DraggableEquipmentItem.instance.inventoryUI.SetData(new EquipmentDataCellUI()
        {
            inventoryIndex = DraggableEquipmentItem.instance.InventoryIndex,
            itemID = InventorySystem.instance.GetItemInventoryID(DraggableEquipmentItem.instance.InventoryIndex),
            itemType = InventorySystem.instance.GetItemInventoryType(DraggableEquipmentItem.instance.InventoryIndex),
        });
        itemID = InventorySystem.instance.GetItemEquipment()[type].GetItemID();
        DraggableEquipmentItem.instance.gameObject.SetActive(false);
        SetData(type);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (itemID <= -1) return;
        TurnActive();
        DraggableEquipmentItem.instance.equipmentUI = this;
        DraggableEquipmentItem.instance.SetVisual(type, itemID, _fromEquipment: true);
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

    public void OnEndDrag(PointerEventData eventData)
    {
        DraggableEquipmentItem.instance.onDrop.Invoke();
    }
    public void SetData(ItemType _type)
    {
        type = _type;
        itemID = InventorySystem.instance.GetItemEquipmentID(_type);
        if (itemID <= -1)
        {
            TurnActive(false);
        }
        else
        {
            TurnActive();
            itemIcon.sprite = InventorySystem.instance.GetItemIcon(type, itemID);
        }

    }

    public void TurnActive(bool _on = true)
    {
        active.SetActive(_on);
        equipmentTypeIdleActive.SetActive(_on == false);
    }


}
