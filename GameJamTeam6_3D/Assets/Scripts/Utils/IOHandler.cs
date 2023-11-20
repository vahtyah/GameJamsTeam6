using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;
using System;

public static class IOHandler 
{
    
    public static void SaveData(object _obj, string _filePath)
    {
        string data = JsonConvert.SerializeObject(_obj);
        byte[] encrypt = EncryptionUtil.instance.EncryptStringToBytes_Aes(data);
        new System.Threading.Thread(() =>
        {
            File.WriteAllBytes(_filePath, encrypt);
        }).Start();
    }

    public static void SaveData<T>(T _obj, string _filePath)
    {
        string data = JsonConvert.SerializeObject(_obj);
        byte[] encrypt = EncryptionUtil.instance.EncryptStringToBytes_Aes(data);
        new System.Threading.Thread(() =>
        {
            File.WriteAllBytes(_filePath, encrypt);
        }).Start();
    }

    public static T LoadData<T>(string _filePath)
    {
        byte[] encryptContent = File.ReadAllBytes(_filePath);
        string decrypt = EncryptionUtil.instance.DecryptStringFromBytes_Aes(encryptContent);
        return JsonConvert.DeserializeObject<T>(decrypt);
    }
    public static object LoadDataObject(string _filePath)
    {
        byte[] encryptContent = File.ReadAllBytes(_filePath);
        string decrypt = EncryptionUtil.instance.DecryptStringFromBytes_Aes(encryptContent);
        return JsonConvert.DeserializeObject(decrypt);
    }
    public static bool CheckFileExist(string _filePath)
    {
        
        return File.Exists(_filePath);
    }
    public static void ResetData(string _filePath)
    {
        try
        {
            File.Delete(_filePath);
            PlayerPrefs.DeleteAll();
            Debug.Log("Reset game data");
            Debug.Log("Reset and Update data to BUILD!!!");
        }
        catch (Exception ex)
        {
            Debug.LogError("Please update and save DATA before build!!!");
            Debug.LogException(ex);
        }
    }
}
