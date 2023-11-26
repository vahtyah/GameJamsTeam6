using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class EnemySpawner : SerializedMonoBehaviour
{
    public static EnemySpawner instance;

    List<Transform> spawnMarkers = new List<Transform>();

    /// <summary>
    /// key : wave, value : qty enemy
    /// </summary>
    Dictionary<int , int> enemyWaveRecord = new Dictionary<int , int>();
    List<IEnemy> enemySpawned = new List<IEnemy>();
    List<int> wavesActive = new List<int>();
    int totalWaveMax;
    int countWaveSpawned;

    private void Awake()
    {
        if (instance == null) instance = this;

    }

    public void StartSetup()
    {
        totalWaveMax = LevelConfig.instance.GetCurrentLevel().GetTotalWaveMax();
        spawnMarkers = IngameManager.instance.mapScene.GetSpawnPoints();
    }

    public void StartSpawning()
    {
        StartCoroutine(IESpawn());
    }

    IEnumerator IESpawn()
    {
        for (int waveId = 0; waveId < totalWaveMax; waveId++)
        {
            if (IsEnoughtWave()) yield break;
            if (wavesActive.Contains(waveId)) continue;
            wavesActive.Add(waveId);
            int ranEnemyWaveType = UnityEngine.Random.Range(0, LevelConfig.instance.GetCurrentLevel().GetTotalAvailableWaves());
            int totalEnemiesInWave = LevelConfig.instance.GetCurrentLevel().GetWaveData(ranEnemyWaveType).amount;
            EnemyID currentEnemyID = LevelConfig.instance.GetCurrentLevel().GetWaveData(ranEnemyWaveType).enemyId;
            enemyWaveRecord.Add(waveId, totalEnemiesInWave);
            for (int j = 0; j < totalEnemiesInWave; j++)
            {
                IEnemy enemy = EnemyPooling.instance.SpawnEnemy(currentEnemyID);
                enemySpawned.Add(enemy);
                enemy.SetThisEnemyFromWave(waveId)
                    .GetGameObject().transform.position = RandomPosition()
                    ;
                yield return new WaitForSeconds(LevelConfig.instance.GetCurrentLevel().GetWaitTimeWave());
            }
            countWaveSpawned++;
        }
    }

    Vector3 RandomPosition()
    {
        Vector3 pos = spawnMarkers[Random.Range(0, spawnMarkers.Count)].position;
        while (Vector3.Distance(pos, IngameManager.instance.player.position) < 5f) {
            pos = spawnMarkers[Random.Range(0, spawnMarkers.Count)].position;
        }
        return pos;
    }

    bool IsEnoughtWave()
    {
        return totalWaveMax <= countWaveSpawned;
    }

    public void OnEnemyDie(int _atWave)
    {
        enemyWaveRecord[_atWave]--;
        if (enemyWaveRecord[_atWave] == 0 )
        {
            countWaveSpawned--;
            StartSpawning();
        }
    }

}
