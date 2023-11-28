using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DraggableEquipmentItem : MonoBehaviour
{
    public static DraggableEquipmentItem instance;

    [SerializeField] Image itemIcon;

    RectTransform rectTransform;

    int itemId;
    public int ItemID => itemId;
    ItemType type;
    public ItemType Type => type;
    int inventoryIndex;
    public int InventoryIndex => inventoryIndex;
    bool fromEquipment;
    public bool FromEquipment =>fromEquipment;


    private void Awake()
    {
        instance = this;
        rectTransform = transform as RectTransform;
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
        gameObject.SetActive(true);
    }

}
