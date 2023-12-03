using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldCurrencyManager : MonoBehaviour
{
    public static GoldCurrencyManager instance;

    const string saveStringText = "/coinCurrency.txt";
    string saveString;

    int coin = 0;


    private void Awake()
    {
        instance = this;
        saveString = Application.persistentDataPath + saveStringText;
        coin = LoadCoin();
    }

    public void AddCoin(int _input)
    {
        coin++;
        SaveCoin();
    }

    void SaveCoin()
    {
        IOSystemic.SaveData<int>(coin, saveString);
    }

    int LoadCoin()
    {
        if (IOSystemic.CheckFileExist(saveString))
        {
            return IOSystemic.LoadData<int>(saveString);
        }
        return 0;
    }

}
