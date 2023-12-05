using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryItemUI : MonoBehaviour, IPointerDownHandler, IDragHandler, IEndDragHandler
    , IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] GameObject active;
    [SerializeField] Image itemIcon;
    [Header("Debug")]
    public int itemID;
    public ItemType type;
    public int inventoryIndex;

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
        Vector2 resultVector = (mousePosition - new Vector2(Input.mousePosition.x, Input.mousePosition.y));
        if (isClick && resultVector.magnitude < 0.5f)
        {
            //show infopanel
            return;
        }
        if (DraggableEquipmentItem.instance.FromEquipment)
        {
            InventorySystem.instance.SwapEquipmentInventoryItem(inventoryIndex, DraggableEquipmentItem.instance.Type);
            DraggableEquipmentItem.instance.equipmentUI.SetData(type);
        }
        else
        {
            InventorySystem.instance.SwapInventoryItems(DraggableEquipmentItem.instance.InventoryIndex, inventoryIndex);
            DraggableEquipmentItem.instance.inventoryUI.SetData(new EquipmentDataCellUI()
            {
                inventoryIndex = DraggableEquipmentItem.instance.InventoryIndex,
                itemID = InventorySystem.instance.GetItemInventoryID(DraggableEquipmentItem.instance.InventoryIndex),
                itemType = InventorySystem.instance.GetItemInventoryType(DraggableEquipmentItem.instance.InventoryIndex),
            }); ;
        }
        DraggableEquipmentItem.instance.gameObject.SetActive(false);
        type = InventorySystem.instance.GetItemInventory()[inventoryIndex].GetItemType();
        itemID = InventorySystem.instance.GetItemInventory()[inventoryIndex].GetItemID();
        SetActive();
        SetItemIcon();
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (itemID <= -1) return;
        active.SetActive(false);
        DraggableEquipmentItem.instance.inventoryUI = this;
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

    public void OnEndDrag(PointerEventData eventData)
    {
        DraggableEquipmentItem.instance.onDrop.Invoke();
        return;
  
    }



    public void SetActive()
    {
        active.SetActive(itemID >= 0);
    }
    EquipmentDataCellUI data;
    public void SetData(EquipmentDataCellUI _data)
    {
        data = _data;
        inventoryIndex = _data.inventoryIndex;
        itemID = _data.itemID;
        type = _data.itemType;
     
        SetItemIcon();
    }

    void SetItemIcon()
    {
        if (itemID <= -1) {
            active.SetActive(false);
            return;
        }
        active.SetActive(true);
        itemIcon.sprite = InventorySystem.instance.GetItemIcon(type, itemID);
    }

}
