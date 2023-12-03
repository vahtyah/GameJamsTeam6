using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GachaItemHolder : MonoBehaviour
{
    [SerializeField] Dictionary<ItemType, IGachaItemOnField> equipmentItemPrefabs;
    [SerializeField] IGachaItemOnField coinPrefab;
    Dictionary<ItemType, List<IGachaItemOnField>> equipmentPool = new Dictionary<ItemType, List<IGachaItemOnField>>();
    List<IGachaItemOnField> coinPool = new List<IGachaItemOnField>();

    public void SpawnEquipmentItem(ItemType _type, int _value, Vector3 _position)
    {
        for (int i = 0; i < equipmentPool[_type].Count; i++)
        {
            if (equipmentPool[_type][i].GetGameObject().activeSelf) continue;
            equipmentPool[_type][i].SetValueQty(_value);
        }

    }




}
public interface IGachaItemOnField
{
    public void SetValueQty(int _value);
    public void Spin();
    public GameObject GetGameObject();
}

