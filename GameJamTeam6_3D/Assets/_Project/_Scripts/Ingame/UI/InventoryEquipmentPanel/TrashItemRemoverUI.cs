using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TrashItemRemoverUI : MonoBehaviour
    , IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] GameObject glowBg;

    bool mouseEntered = false;

    void Start()
    {
        DraggableEquipmentItem.instance.onDrop += OnDrop;
        glowBg.SetActive(false);
    }

    void OnDrop()
    {
        if (mouseEntered == false) return;
        if (DraggableEquipmentItem.instance.FromEquipment) return;
        InventorySystem.instance.RemoveInventory(DraggableEquipmentItem.instance.InventoryIndex);

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        mouseEntered = true;
        glowBg.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        mouseEntered = false;
        glowBg.SetActive(false);
    }
}
