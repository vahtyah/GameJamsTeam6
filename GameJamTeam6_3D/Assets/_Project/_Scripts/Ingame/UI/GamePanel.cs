using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePanel : MonoBehaviour, IGameSignal
{
    public static GamePanel instance;
    [SerializeField] GameObject hud;
    [SerializeField] InventoryPanel inventoryEquipment;
    [SerializeField] GameObject pausePanel;
    [SerializeField] GameObject winPanel;
    [SerializeField] GameObject losePanel;

    public GameObject GetHUDObj() { return hud; }

    private void Awake()
    {
        instance = this; 
    }

    private void Update()
    {
        if (InputHandler.instance.PressInventory()) ActiveInventory();
        if (InputHandler.instance.PressPause()) PausePanelHandle();
    }

    public void ActiveInventory()
    {
        inventoryEquipment.SetActivePanel(inventoryEquipment.GetActivePanel().gameObject.activeSelf == false);
        PausePanelMechanic(inventoryEquipment.GetActivePanel().gameObject.activeSelf);
    }

    private void PausePanelHandle()
    {
        pausePanel.SetActive(pausePanel.activeSelf == false);
        PausePanelMechanic(pausePanel.activeSelf);
    }

    void PausePanelMechanic(bool _isActive)
    {
        if (_isActive == false) IngameManager.instance.Resume();
        else IngameManager.instance.Pause();
    }

    public void Prepare() { }

    public void StartGame() { Cursor.visible = false; }

    public void Pause()
    {
        Cursor.visible = true;
    }

    public void Resume()
    {
        Cursor.visible = false;
    }

    public void Win()
    {
        winPanel.SetActive(true);
        Cursor.visible = true;
    }

    public void Lose()
    {
        losePanel.SetActive(true);
        Cursor.visible = true;
    }
}