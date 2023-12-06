using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSceneManager : MonoBehaviour
{
    public static MapSceneManager instance;

    const string playerProgressSaveTxt = "/playerMapProgress.txt";
    string playerProgressSave;
    [ListDrawerSettings(ShowIndexLabels = true)]
    [SerializeField] MapScene[] mapScenePrefabs;
    [Header("Debug")][SerializeField] MapScene currentMap;
    public MapScene GetCurrentMap() { return currentMap; }
    int currentMapID = 0;
    
    private void Awake()
    {
        instance = this;
        playerProgressSave = Application.persistentDataPath + playerProgressSaveTxt;
    }

    private void Start()
    {
        LoadScene(LoadPLayerSceneProgress());
    }

    public void LoadScene(int _mapID)
    {
        EnemySpawner.instance.StopSpawning();
        currentMapID = _mapID;
        if (currentMap != null)
        {
            currentMap.gameObject.SetActive(false);
            Destroy(currentMap.gameObject);
        }
        currentMap = Instantiate(mapScenePrefabs[currentMapID]);
        IngameManager.instance.Prepare();
        SavePlayerSceneProgress();
    }

    void SavePlayerSceneProgress()
    {
        IOSystemic.SaveData<int>(currentMapID, playerProgressSave);
    }

    int LoadPLayerSceneProgress()
    {
        if (IOSystemic.CheckFileExist(playerProgressSave))
        {
            return IOSystemic.LoadData<int>(playerProgressSave);
        }
        return 0;
    }


}
