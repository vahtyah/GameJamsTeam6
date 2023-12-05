using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryButtonHUD : MonoBehaviour
{
    [SerializeField] Button self;

    private void Awake()
    {
        self = GetComponent<Button>();
        self.onClick.AddListener(() => { GamePanel.instance.ActiveInventory(); });
    }

}
