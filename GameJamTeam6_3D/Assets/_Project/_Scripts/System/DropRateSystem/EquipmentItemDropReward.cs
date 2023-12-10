using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class EquipmentItemDropReward : SerializedMonoBehaviour, IDropReward
{
    [Range(0f, 100f)]
    [SerializeField] float rate = 0.9f;
    [Tooltip("Value : rate")]
    [SerializeField] Dictionary<ItemType, float> itemRates = new Dictionary<ItemType, float>() {
        {ItemType.Weapon, 0.5f }
    };
    [ListDrawerSettings(ShowIndexLabels =  true)]
    [SerializeField] int[] equipIdRates = new int[1] {25 };
    [SerializeField] IEnemy enemy;

    void Awake()
    {
        if (GetComponent<IEnemy>() != null)
        {
            enemy.GetCharacterHealth().onDead += DoGacha;
        }
        
    }

    public void DoGacha()
    {
        if (rate == 0) return;
        if (Random.Range(1, 101f) > rate) return;
        for (int tryTime = 0; tryTime < 10; tryTime++)
        {
            ItemType typeRoll = RollItemKey();
            if (itemRates[typeRoll] == 0) continue;
            float ranRate = Random.Range(1f, 101f);
            if (Random.Range(1, 101f) > ranRate) continue;
            int id = RollEquipID();
            DropItemHolder.instance.SpawnEquipmentItem(typeRoll, id, transform.position);
            return;
        }
    }

    ItemType RollItemKey()
    {
        int index = Random.Range(0, itemRates.Keys.Count);
        int count = 0;
        foreach (var item in itemRates.Keys)
        {
            if (count == index)
            {
                return item;
            }
            count++;
        }
        return ItemType.Weapon;
    }

    int RollEquipID()
    {
        if (equipIdRates.Length == 1) return 0;
        for (int tryTime = 0; tryTime < 10; tryTime++)
        {
            int ranID = Random.Range(0, equipIdRates.Length);
            float value = Random.Range(1, 101f);
            if (value <= equipIdRates[ranID])
            {
                return ranID;
            }
        }
        return 0;
    }
}


