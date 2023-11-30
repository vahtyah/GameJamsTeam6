using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GlovesData", menuName = GlobalString.ItemData + "GlovesData")]
public class GlovesData : ScriptableObject, IItemEquipmentData
{
    [SerializeField] Sprite itemIcon;
    [SerializeField] string itemName;
    [SerializeField] string description;
    public string GetItemDescription()
    {
        
    }

    public Sprite GetItemIcon()
    {
        return itemIcon;
    }

    public int GetItemID()
    {
        
    }

    public string GetItemName()
    {
        
    }

    public ItemType GetItemType()
    {
        
    }

    public void OnEquip()
    {
        
    }

    public void OnUnEquip()
    {
       
    }

    public void SetItemID(int _id)
    {
       
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
