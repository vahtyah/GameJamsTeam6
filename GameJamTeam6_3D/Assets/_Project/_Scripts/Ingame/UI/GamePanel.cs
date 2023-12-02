using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePanel : MonoBehaviour
{
    public static GamePanel instance;
    [SerializeField] GameObject inventoryEquipment;

    private void Awake()
    {
        instance = this; 
    }

    private void Update()
    {
        if (InputHandler.instance.PressInventory()) ActiveInventory();
    }

    public void ActiveInventory()
    {
        inventoryEquipment.SetActive(inventoryEquipment.gameObject.activeSelf == false);
        if (inventoryEquipment.gameObject.activeSelf == false)
        {

        }
        else
        {

        }
    }


}
