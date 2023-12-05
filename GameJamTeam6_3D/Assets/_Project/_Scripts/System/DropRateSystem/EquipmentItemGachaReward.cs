using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class EquipmentItemGachaReward : SerializedMonoBehaviour, IGachaReward
{
    [Range(0f, 100f)]
    [SerializeField] float rate;
    [SerializeField] Dictionary<ItemType, float> itemRates = new Dictionary<ItemType, float>();
    [SerializeField] int[] equipIdRates;
    [SerializeField] int qty = 1;

    public void DoGacha()
    {
        if (rate == 0) return;
        if (Random.Range(1, 101f) > rate) return;
        int count = 0;
        while (true)
        {
            int ranItemType = Random.Range(0, itemRates.Keys.Count);
            if (itemRates[(ItemType)ranItemType] == 0) continue;
            float ranRate = Random.Range(1f, 101f);
            if (Random.Range(1, 101f) > rate) continue;
            int id = RollEquipID();
            DropItemHolder.instance.SpawnEquipmentItem((ItemType)ranItemType, qty, id, transform.position);
            count++;
            if (count >= qty) break;
        }
    }

    int RollEquipID()
    {
        if (equipIdRates.Length == 1) return 0;
        while (equipIdRates.Length > 1)
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


