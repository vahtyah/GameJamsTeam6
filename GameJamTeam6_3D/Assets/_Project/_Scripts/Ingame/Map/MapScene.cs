using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapScene : MonoBehaviour
{
    [SerializeField] Transform spawnPointHolder;
    List<Transform> spawnPoints = new List<Transform>();
    public List<Transform> GetSpawnPoints() { return spawnPoints; }

    private void Awake()
    {
        for (int i = 0; i < spawnPointHolder.childCount; i++)
        {
            spawnPoints.Add(spawnPointHolder.GetChild(i).transform);
        }
    }


}
