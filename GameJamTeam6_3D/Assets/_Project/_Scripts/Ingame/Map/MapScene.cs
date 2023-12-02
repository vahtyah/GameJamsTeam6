using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapScene : MonoBehaviour
{
    public static MapScene instance;
    [SerializeField] Transform playerStartPos;
    [SerializeField] MapEnemySpawnData mapEnemySpawnData;
    [SerializeField] MapTransfer previousMap;
    [SerializeField] MapTransfer nextMap;
    [SerializeField] Transform spawnPointHolder;

    List<Transform> spawnPoints = new List<Transform>();

    public MapTransfer GetPreviousMap() => previousMap;
    public MapTransfer GetNextMap() => nextMap;
    public MapEnemySpawnData GetSpawnData() { return mapEnemySpawnData; }
    public List<Transform> GetSpawnPoints() { return spawnPoints; }

    private void Awake()
    {
        instance = this;
        for (int i = 0; i < spawnPointHolder.childCount; i++)
        {
            spawnPoints.Add(spawnPointHolder.GetChild(i).transform);
        }
    }

    private void Start()
    {
        Player.instance.transform.position = playerStartPos.position;
        previousMap.TurnOn();
        nextMap.TurnOn();
    }


}
