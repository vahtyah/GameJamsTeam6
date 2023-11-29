using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EquipmentItemUI : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
    , IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] GameObject active;
    [SerializeField] Image itemIcon;

    int itemID;
    ItemType type;

    bool mouseEnter;
    bool isClick = false;
    Vector2 mousePosition;

    public void OnDrag(PointerEventData eventData)
    {
        if (itemID <= -1) return;
        TurnOff();
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

    public void OnPointerUp(PointerEventData eventData)
    {

    }
    public void SetData(ItemType _type, int id)
    {
        if (id <= -1)
        {
            TurnOff();
        }
        else
        {
            active.SetActive(true);
            itemID = id;
            type = _type;
            itemIcon.sprite = InventorySystem.instance.GetItemHolders()[_type][id].GetItemIcon();
        }

    }

    public void TurnOff()
    {
        active.SetActive(false);
    }


}
