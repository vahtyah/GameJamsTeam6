using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePanel : MonoBehaviour, IGameSignal
{
    public static GamePanel instance;
    [SerializeField] GameObject inventoryEquipment;
    [SerializeField] GameObject pausePanel;
    [SerializeField] GameObject winPanel;
    [SerializeField] GameObject losePanel;

    private void Awake()
    {
        instance = this; 
    }

    private void Update()
    {
        if (InputHandler.instance.PressInventory()) ActiveInventory();
        if (InputHandler.instance.PressPause()) PauseHandle();
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

    private void PauseHandle()
    {
        if (IngameManager.instance.gameState == GameState.Pause)
        {
            IngameManager.instance.Resume();
        }
        else
        {
            IngameManager.instance.Pause();
        }
    }

    public void Prepare() { }

    public void StartGame() { }

    public void Pause()
    {
        pausePanel.SetActive(true); 
        Time.timeScale = 0;
    }

    public void Resume()
    {
        pausePanel.SetActive(false); 
        Time.timeScale = 1;
    }

    public void Win()
    {
        winPanel.SetActive(true);
    }

    public void Lose()
    {
        losePanel.SetActive(true);
    }
}