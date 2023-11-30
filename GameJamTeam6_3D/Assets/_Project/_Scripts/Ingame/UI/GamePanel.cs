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
        if (InputHandler.instance.PressInventory())
        {
            inventoryEquipment.SetActive(inventoryEquipment.gameObject.activeSelf == false);
        }
    }



}
