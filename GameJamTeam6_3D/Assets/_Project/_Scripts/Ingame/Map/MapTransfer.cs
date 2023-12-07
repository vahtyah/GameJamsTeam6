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
        if (other.CompareTag(GlobalInfo.tagPlayer))
        {
            LoadingPanel.instance.LoadingMidScene(() => IngameManager.instance.StartGame());
            MapSceneManager.instance.LoadScene(nextMapID);
        }
    }


}
