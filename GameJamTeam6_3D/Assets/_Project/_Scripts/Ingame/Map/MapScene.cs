using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapScene : MonoBehaviour
{
    [SerializeField] Transform playerStartPos;
    [SerializeField] MapEnemySpawnData mapEnemySpawnData;
    [SerializeField] MapTransfer previousMapCheckPoint;
    [SerializeField] MapTransfer nextMapCheckpoint;
    [SerializeField] Transform spawnPointHolder;

    List<Transform> spawnPoints = new List<Transform>();

    public MapEnemySpawnData GetSpawnData() { return mapEnemySpawnData; }
    public List<Transform> GetSpawnPoints() { return spawnPoints; }

    private void Awake()
    {
        //instance = this;
        for (int i = 0; i < spawnPointHolder.childCount; i++)
        {
            spawnPoints.Add(spawnPointHolder.GetChild(i).transform);
        }
    }

    private void Start()
    {
        Player.instance.transform.position = playerStartPos.position;
    }

    public void ActivePortals(bool _yep = true)
    {
        previousMapCheckPoint.gameObject.SetActive(_yep);
        nextMapCheckpoint.gameObject.SetActive(_yep);
    }
}
