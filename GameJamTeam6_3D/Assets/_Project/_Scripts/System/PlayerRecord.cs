using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRecord : SerializedMonoBehaviour
{
    public static PlayerRecord instance;

    const string playerHealthTxt = "/playerHealth.txt";
    string playerHealthSaveString;
    //[ListDrawerSettings(ShowIndexLabels = true)]
    //[SerializeField] PlayerAchievementData playerAchievementData;

    private void Awake()
    {
        instance = this;
        playerHealthSaveString = Application.persistentDataPath + playerHealthTxt;
    }

    public int GetPlayerHealth()
    {
        if (IOSystemic.CheckFileExist(playerHealthSaveString))
        {
            return IOSystemic.LoadData<int>(playerHealthSaveString);
        }
        return Player.instance.GetPlayerData().maxHp.value;
    }




}

public class PlayerAchievementData
{
    public string name;
    public string description;
}