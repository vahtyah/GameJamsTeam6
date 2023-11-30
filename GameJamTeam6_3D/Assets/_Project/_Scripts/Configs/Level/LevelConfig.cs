using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelConfig : MonoBehaviour
{
    public static LevelConfig instance;
    const string currentLevelSaveString = "/save_current_level.txt";

    string savedCurrentLevel;
    [SerializeField] Level[] levels;
    int currentLevel = 0;
    
    public Action<int> onLevelChange;

    private void Awake()
    {
        savedCurrentLevel = Application.persistentDataPath + currentLevelSaveString;
        if (instance == null) instance = this;
        currentLevel = GetCurrentLevelIndex();
    }

    private void Start()
    {
        onLevelChange?.Invoke(currentLevel);
    }

    public void GoNextLevel()
    {
        currentLevel++;
        IOSystemic.SaveData<int>(currentLevel, savedCurrentLevel);
        onLevelChange?.Invoke(currentLevel);
    }

    public Level GetCurrentLevel()
    {
        return levels[currentLevel];
    }

    int GetCurrentLevelIndex()
    {
        if (IOSystemic.CheckFileExist(savedCurrentLevel))
        {
            return IOSystemic.LoadData<int>(savedCurrentLevel); 
        }
        return 0;
    }




}
