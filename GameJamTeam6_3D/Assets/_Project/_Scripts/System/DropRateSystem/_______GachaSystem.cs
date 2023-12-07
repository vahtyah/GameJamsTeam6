using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GachaSystem : SerializedMonoBehaviour
{
    public static GachaSystem instance;

    private void Awake()
    {
        instance = this; 
    }

    public void DoGacha(float _rate, GachaType _type)
    {
        _rate = _rate * 100f;
        float ranResult = Random.Range(0f, 100f);
        if (ranResult > _rate) return;
        switch (_type)
        {
            case GachaType.WeaponItem:
                GachaEquipmentItem();
                break;
            case GachaType.Coin:

                break;
        }

    }

    void GachaEquipmentItem()
    {

    }



}


