using System.Collections;
using System.Collections.Generic;
using _Project._Scripts.Ingame.Manager;
using UnityEngine;
using DG.Tweening;
using Sirenix.OdinInspector;
public class DropEquipmentItemOnField : MonoBehaviour, IDropEquipmentItemOnField
{
    [SerializeField] ItemType itemType;
    [SerializeField] GameObject[] itemIDs;
    //[Header("Debug")]
    //[SerializeField]
    int equipID = 0;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(GlobalInfo.tagPlayer) == false) return;
        InventorySystem.instance.AddItemToInventory(itemType, equipID);
        gameObject.SetActive(false);
        SoundManager.Instance.PlaySFX(Sound.Pickup);
    }


    public GameObject GetGameObject()
    {
        return gameObject;
    }


    public void Activate()
    {
        gameObject.SetActive(true);
        transform.DORotate(new Vector3(0, 360, 0), 0.5f).SetLoops(-1, LoopType.Incremental);
    }
    [Button]
    public void SetEquipmentID(int _id)
    {
        equipID = _id;
        for (int i = 0; i < itemIDs.Length; i++)
        {
            if (i == equipID)
            {
                itemIDs[i].SetActive(true);
                continue;
            }
            itemIDs[i].SetActive(false);
        }
    }
}
