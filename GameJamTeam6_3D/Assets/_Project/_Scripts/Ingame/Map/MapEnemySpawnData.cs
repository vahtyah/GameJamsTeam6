using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
[CreateAssetMenu(fileName = "MapEnemySpawnData 1", menuName = "MapEnemySpawnData")]
public class MapEnemySpawnData : SerializedScriptableObject
{
    [SerializeField] EnemySpawnData spawnData;

    public int GetTotalWaveMax() => spawnData.totalWaveMax;
    public float GetWaitTimeWave() => spawnData.waitTimeWave;
    public SpawnWaveData GetWaveData(int _wave) => spawnData.waveDatas[_wave];
    public int GetTotalAvailableWaves()=> spawnData.waveDatas.Count;
}

public class EnemySpawnData
{
    public List<SpawnWaveData> waveDatas;
    [Space]
    [Space]
    public int totalWaveMax = 5;
    public float waitTimeWave = 1f;
}

public class SpawnWaveData
{
    public EnemyID enemyId;
    public int amount;

}

public enum EnemyID
{
    Example = 0, two = 1, three = 2//, four = 3,
}

