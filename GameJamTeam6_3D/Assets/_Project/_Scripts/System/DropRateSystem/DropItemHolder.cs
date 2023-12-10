using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class DropItemHolder : SerializedMonoBehaviour
{
    public static DropItemHolder instance;

    [SerializeField] Dictionary<ItemType, IDropEquipmentItemOnField> equipmentItemPrefabs;
    [SerializeField] IDropCurrencyItemOnField coinPrefab;
    Dictionary<ItemType, List<IDropEquipmentItemOnField>> equipmentPool = new Dictionary<ItemType, List<IDropEquipmentItemOnField>>() {
        {ItemType.Armour, new List<IDropEquipmentItemOnField>() },
        {ItemType.Weapon, new List<IDropEquipmentItemOnField>() },
        {ItemType.Helmet, new List<IDropEquipmentItemOnField>() },
        {ItemType.Gloves, new List<IDropEquipmentItemOnField>() },
        {ItemType.Shoes, new List<IDropEquipmentItemOnField>() },
        {ItemType.Belt, new List<IDropEquipmentItemOnField>() },
    };
    List<IDropCurrencyItemOnField> coinPool = new List<IDropCurrencyItemOnField>();

    private void Awake()
    {
        instance = this;
    }

    public void SpawnEquipmentItem(ItemType _type, int _equipId, Vector3 _position)
    {
        IDropEquipmentItemOnField dropEquipment = GenEquipmentDropped(_type, _position);
        dropEquipment.SetEquipmentID(_equipId);
        dropEquipment.Activate();
    }

    IDropEquipmentItemOnField GenEquipmentDropped(ItemType _type, Vector3 _position)
    {
        for (int i = 0; i < equipmentPool[_type].Count; i++)
        {
            if (equipmentPool[_type][i].GetGameObject().activeSelf) continue;
            equipmentPool[_type][i].GetGameObject().transform.position = _position;
            equipmentPool[_type][i].Activate();
            return equipmentPool[_type][i];
        }
        IDropEquipmentItemOnField newObj = Instantiate(equipmentItemPrefabs[_type].GetGameObject(), transform).GetComponent<IDropEquipmentItemOnField>();
        newObj.GetGameObject().transform.position = _position;
        equipmentPool[_type].Add(newObj);
        newObj.Activate();
        return newObj;
    }

    public void SpawnGold(int _value, Vector3 _position)
    {
        for (int i = 0; i < coinPool.Count; i++)
        {
            if (coinPool[i].GetGameObject().activeSelf) continue;
            coinPool[i].GetGameObject().transform.position = _position;
            coinPool[i].SetValue(_value);
            coinPool[i].Activate();
            return;
        }
        IDropCurrencyItemOnField newObj = Instantiate(coinPrefab.GetGameObject(), transform).GetComponent<IDropCurrencyItemOnField>();
        newObj.GetGameObject().transform.position = _position;
        coinPool.Add(newObj);
        newObj.SetValue(_value);
        newObj.Activate();
    }



}
public interface IDropItemOnField
{
   
    public void Activate();
    public GameObject GetGameObject();
}

public interface IDropCurrencyItemOnField : IDropItemOnField
{
    public void SetValue(int _value);
}

public interface IDropEquipmentItemOnField : IDropItemOnField
{
    public void SetEquipmentID(int _id);
}

