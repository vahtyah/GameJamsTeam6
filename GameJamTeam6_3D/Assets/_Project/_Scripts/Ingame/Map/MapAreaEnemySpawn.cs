using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapAreaEnemySpawn : MonoBehaviour
{
    [SerializeField] EnemySpawnType spawnType;
    [SerializeField] EnemyID[] enemyIDs;
    [SerializeField] int qty;
    List<Transform> spawns = new List<Transform>();

    private void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            spawns.Add(transform.GetChild(i));
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(GlobalString.tagPlayer) == false) return;
        switch (spawnType)
        {
            case EnemySpawnType.Arc:
                SpawnArc();
                break;
            case EnemySpawnType.Single:
                SingleSpawn();
                break;
            case EnemySpawnType.Group:
                SpawnGroup();
                break;
        }
    }

    void SingleSpawn()
    {
        for (int i = 0; i < qty; i++)
        {
            IEnemy enemy = EnemyPooling.instance.SpawnEnemy(enemyIDs[Random.Range(0, enemyIDs.Length)]);
            enemy.SetThisEnemyFromWave(-1)
                .GetGameObject().transform.position = RandomPosition()
                ;
        }
    }

    void SpawnArc()
    {
        StartCoroutine( EnemySpawner.instance.IESpawnArcShape(RandomPosition()));
    }

    void SpawnGroup()
    {
        StartCoroutine(EnemySpawner.instance.IESpawnGroup(RandomPosition()));
    }

    Vector3 RandomPosition()
    {
        return spawns[ Random.Range(0, spawns.Count)].position;
    }


}
