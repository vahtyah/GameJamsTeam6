using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class DropEquipmentItemOnField : MonoBehaviour, IDropEquipmentItemOnField
{
    [SerializeField] ItemType itemType;
    [SerializeField] GameObject[] itemIDs;
    int equipID = 0;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(GlobalInfo.tagPlayer) == false) return;
        InventorySystem.instance.AddItemToInventory(itemType, equipID);
        gameObject.SetActive(false);
    }


    public GameObject GetGameObject()
    {
        return gameObject;
    }


    public void Spin()
    {
        gameObject.SetActive(true);
        //float ranPosX = Random.Range(-0.5f + transform.position.x, 0.5f + transform.position.x);
        //float ranPosZ = Random.Range(-0.5f + transform.position.z, 0.5f + transform.position.z);
        //transform.DOMove(new Vector3(ranPosX, transform.position.y + 1f, ranPosZ), 0.5f).SetEase(Ease.Linear);
        transform.DORotate(new Vector3(0, 360, 0), 0.5f).SetLoops(-1, LoopType.Incremental);
    }

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
