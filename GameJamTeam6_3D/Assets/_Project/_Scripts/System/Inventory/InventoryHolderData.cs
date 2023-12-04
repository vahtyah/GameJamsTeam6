using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "InventoryHolderData", menuName = "InventoryHolderData")]
public class InventoryHolderData : SerializedScriptableObject
{
    public Dictionary<ItemType, ListIItemData> itemHolding;
}
