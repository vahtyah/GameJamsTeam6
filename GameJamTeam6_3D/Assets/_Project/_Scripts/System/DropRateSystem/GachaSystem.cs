using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GachaSystem : SerializedMonoBehaviour
{
    [SerializeField]  Dictionary<ItemType, IGachaItemOnField> equipmentItemPrefabs;
    [SerializeField] IGachaItemOnField coinPrefab;

    public void DoGacha(float _rate)
    {
        _rate = _rate * 100f;
        float ranResult = Random.Range(0f, 100f);
        if (ranResult < _rate)
        {

        }
        else
        {

        }
    }

    void GachaEquipmentItem()
    {

    }



}


