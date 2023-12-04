using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using UnityEngine;

public class EncryptionUtil : MonoBehaviour
{
    public static EncryptionUtil instance;

    //public bool EncrytionAwaken{ private set; get; }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            LoadKey();
            //if (GetKey() == false)
            //{
            //    GenerateKey();
            //}
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(this);
    }

    public void LoadKey()
    {
        if (GetKey() == false)
        {
            GenerateKey();
        }
    }

    private static byte[] Key;
    private static byte[] IV;
    private void GenerateKey()
    {
        using (Aes aesAlg = Aes.Create())
        {
            aesAlg.GenerateKey();
            aesAlg.GenerateIV();
            Key = aesAlg.Key;
            IV = aesAlg.IV;
        }
        // save key
        SaveKey(Key, IV);
    }

    private void SaveKey(byte[] key, byte[] iv)
    {
        string bonusKey = Convert.ToBase64String(key).Insert(6, RandomBonusString[UnityEngine.Random.Range(0, RandomBonusString.Count)]);
        string bonusIV = Convert.ToBase64String(iv).Insert(6, RandomBonusString[UnityEngine.Random.Range(0, RandomBonusString.Count)]);
        string dataPath = Application.persistentDataPath + "/EncryptData.txt";
        new System.Threading.Thread(() =>
        {
            //File.WriteAllBytes(dataPath, key);
            File.WriteAllLines(dataPath, new List<string>()
            {
                bonusKey,
                bonusIV
            }, System.Text.Encoding.UTF8);
        }).Start();
    }

    private List<string> RandomBonusString = new List<string>()
    {
        "aer", "etd", "dgf", "rd3", "6xu", "lk4", "s8z", "gdw", "gt1"
    };

    private bool GetKey()
    {
        string dataPath = Application.persistentDataPath + "/EncryptData.txt";
        if (System.IO.File.Exists(dataPath))
        {
            string[] dataContents = System.IO.File.ReadAllLines(dataPath);
            Key = Convert.FromBase64String(dataContents[0].Remove(6, 3));
            IV = Convert.FromBase64String(dataContents[1].Remove(6, 3));
            return true;
        }
        else
        {
            return false;
        }
    }


    public byte[] EncryptStringToBytes_Aes(string plainText)
    {
        // Check arguments.
        if (plainText == null || plainText.Length <= 0)
            throw new ArgumentNullException("plainText");
        byte[] encrypted;

        // Create an Aes object
        // with the specified key and IV.
        using (Aes aesAlg = Aes.Create())
        {
            aesAlg.Key = Key;
            aesAlg.IV = IV;

            // Create an encryptor to perform the stream transform.
            ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

            // Create the streams used for encryption.
            using (MemoryStream msEncrypt = new MemoryStream())
            {
                using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                {
                    using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                    {
                        //Write all data to the stream.
                        swEncrypt.Write(plainText);
                    }
                    encrypted = msEncrypt.ToArray();
                }
            }
        }
        // Return the encrypted bytes from the memory stream.
        return encrypted;
    }

    public string DecryptStringFromBytes_Aes(byte[] cipherText)
    {
        // Check arguments.
        if (cipherText == null || cipherText.Length <= 0)
            throw new ArgumentNullException("cipherText");

        // Declare the string used to hold
        // the decrypted text.
        string plaintext = null;

        // Create an Aes object
        // with the specified key and IV.
        using (Aes aesAlg = Aes.Create())
        {
            aesAlg.Key = Key;
            aesAlg.IV = IV;

            // Create a decryptor to perform the stream transform.
            ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

            // Create the streams used for decryption.
            using (MemoryStream msDecrypt = new MemoryStream(cipherText))
            {
                using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                {
                    using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                    {

                        // Read the decrypted bytes from the decrypting stream
                        // and place them in a string.
                        plaintext = srDecrypt.ReadToEnd();
                    }
                }
            }
        }
        return plaintext;
    }
}
