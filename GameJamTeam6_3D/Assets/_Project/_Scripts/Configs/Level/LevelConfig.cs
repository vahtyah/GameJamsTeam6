using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelConfig : MonoBehaviour
{
    public static LevelConfig instance;

    string savedCurrentLevel;
    [SerializeField] Level[] levels;
    int currentLevel = 0;

    private void Awake()
    {
        savedCurrentLevel = Application.persistentDataPath + "/save_current_level.txt";
        if (instance == null) instance = this;
        currentLevel = GetCurrentLevelIndex();
    }

    public void GoNextLevel()
    {
        currentLevel++;
        IOHandler.SaveData<int>(currentLevel, savedCurrentLevel);
    }

    public Level GetCurrentLevel()
    {
        return levels[currentLevel];
    }

    int GetCurrentLevelIndex()
    {
        if (IOHandler.CheckFileExist(savedCurrentLevel))
        {
            return IOHandler.LoadData<int>(savedCurrentLevel); 
        }
        return 0;
    }




}
