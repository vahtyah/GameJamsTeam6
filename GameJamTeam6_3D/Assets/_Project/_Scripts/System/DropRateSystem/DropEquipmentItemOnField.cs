using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class DropEquipmentItemOnField : MonoBehaviour, IDropEquipmentItemOnField
{
    [SerializeField] ItemType itemType;
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
        float ranPosX = Random.Range(-0.5f + transform.position.x, 0.5f + transform.position.x);
        float ranPosZ = Random.Range(-0.5f + transform.position.z, 0.5f + transform.position.z);
        transform.DOMove(new Vector3(ranPosX, transform.position.y + 0.2f, ranPosZ), 0.25f).SetEase(Ease.InCirc);


    }

    public void SetEquipmentID(int _id)
    {
        equipID = _id;
    }
}
