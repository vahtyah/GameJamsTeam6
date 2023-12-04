using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DraggableEquipmentItem : MonoBehaviour
{
    public static DraggableEquipmentItem instance;

    public Action onDrop;

    [SerializeField] Image itemIcon;

    RectTransform rectTransform;
    [Header("Debug")]
    [SerializeField] int itemId;
    public int ItemID => itemId;
    ItemType type;
    public ItemType Type => type;
    [SerializeField] int inventoryIndex;
    public int InventoryIndex => inventoryIndex;
    [SerializeField] bool fromEquipment;
    public bool FromEquipment =>fromEquipment;

    public EquipmentItemUI equipmentUI;
    public InventoryItemUI inventoryUI;

    bool ok = false;

    private void Awake()
    {
        instance = this;
        rectTransform = transform as RectTransform;
        gameObject.SetActive(false);
    }

    private void Update()
    {
        rectTransform.position = Input.mousePosition;
    }

    public void SetVisual(ItemType _type, int _id, bool _fromEquipment , int _inventoryIndex = -1)
    {
        fromEquipment = _fromEquipment;
        itemId = _id;
        type = _type;
        inventoryIndex = _inventoryIndex;
        itemIcon.sprite = InventorySystem.instance.GetItemIcon(_type, _id);
        rectTransform.position = Input.mousePosition;
        gameObject.SetActive(true);
    }

    public void Restore()
    {
        if (fromEquipment)
        {
            equipmentUI.TurnActive(_on: true);
        }
        else
        {
            inventoryUI.SetActive();
        }
    }

}
