using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapTransfer : MonoBehaviour
{
    [SerializeField] int nextMapID = -1;

    public void TurnOn()
    {
        gameObject.SetActive(true);
    }

    public void TurnOff()
    {
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(GlobalString.tagPlayer))
        {
            MapSceneManager.instance.LoadScene(nextMapID);
        }
    }


}
