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

    private IEnumerator IESpawnArcShape()
    {
        for (int waveId = 0; waveId < totalWaveMax; waveId++)
        {
            if (IsEnoughtWave()) yield break;
            if (wavesActive.Contains(waveId)) continue;
            wavesActive.Add(waveId);
            var currentLevel = LevelConfig.instance.GetCurrentLevel();
            int ranEnemyWaveType =
                UnityEngine.Random.Range(0, currentLevel.GetTotalAvailableWaves());
            int totalEnemiesInWave = currentLevel.GetWaveData(ranEnemyWaveType).amount;
            EnemyID currentEnemyID = currentLevel.GetWaveData(ranEnemyWaveType).enemyId;
            enemyWaveRecord.Add(waveId, totalEnemiesInWave);
            var IEnemy = EnemyPooling.instance.GetIEnemy(currentEnemyID);
            var positions = GetArcShapePositions(totalEnemiesInWave, IEnemy);
            for (int j = 0; j < totalEnemiesInWave; j++)
            {
                var enemy = EnemyPooling.instance.SpawnEnemy(currentEnemyID);
                enemySpawned.Add(enemy);
                enemy.SetThisEnemyFromWave(waveId)
                    .GetGameObject().transform.position = positions[j];
            }

            countWaveSpawned++;
        }
    }
    
    private IEnumerator IESpawnGroup()
    {
        for (int waveId = 0; waveId < totalWaveMax; waveId++)
        {
            if (IsEnoughtWave()) yield break;
            if (wavesActive.Contains(waveId)) continue;
            wavesActive.Add(waveId);
            var currentLevel = LevelConfig.instance.GetCurrentLevel();
            int ranEnemyWaveType =
                UnityEngine.Random.Range(0, currentLevel.GetTotalAvailableWaves());
            int totalEnemiesInWave = currentLevel.GetWaveData(ranEnemyWaveType).amount;
            EnemyID currentEnemyID = currentLevel.GetWaveData(ranEnemyWaveType).enemyId;
            enemyWaveRecord.Add(waveId, totalEnemiesInWave);
            var IEnemy = EnemyPooling.instance.GetIEnemy(currentEnemyID);
            var positions = GetGroupPosition(totalEnemiesInWave, IEnemy);
            for (int j = 0; j < totalEnemiesInWave; j++)
            {
                var enemy = EnemyPooling.instance.SpawnEnemy(currentEnemyID);
                enemySpawned.Add(enemy);
                enemy.SetThisEnemyFromWave(waveId)
                    .GetGameObject().transform.position = positions[j];
            }

            countWaveSpawned++;
        }
    }

    Vector3 RandomPosition()
    {
        Vector3 pos = spawnMarkers[Random.Range(0, spawnMarkers.Count)].position;
        while (Vector3.Distance(pos, IngameManager.instance.player.position) < 5f)
        {
            pos = spawnMarkers[Random.Range(0, spawnMarkers.Count)].position;
        }

        return pos;
    }

    List<Vector3> GetGroupPosition(int _qty, IEnemy enemy)
    {
        var spawnPosition = RandomPosition();
        var positions = new List<Vector3>();
        var collider = enemy.GetGameObject().GetComponent<BoxCollider>();
        Debug.Log("collider = " + collider.size);
        var size = Mathf.Max(collider.size.x, collider.size.z);
        Debug.Log("size = " + size);
        
        var rows = Mathf.CeilToInt(Mathf.Sqrt(_qty));
        var cols = 0;
        while (positions.Count < _qty)
        {
            for (var i = 0; i < rows; i++)
            {
                positions.Add(spawnPosition + size * Vector3.right * i - cols * size * Vector3.forward);
            }

            cols++;
        }

        return positions;
    }

    List<Vector3> GetArcShapePositions(int qty, IEnemy enemy)
    {
        var collider = enemy.GetGameObject().GetComponent<BoxCollider>();
        var size = Mathf.Max(collider.size.x, collider.size.z);
        var radius = 2 * size;
        var angle = Mathf.Acos((radius * radius * 2 - size * size) / (2 * radius * radius)) * Mathf.Rad2Deg;
        while (angle * qty > 360f)
        {
            radius += size;
            angle = Mathf.Acos((radius * radius * 2 - size * size) / (2 * radius * radius)) * Mathf.Rad2Deg;
        }
        print(radius + " " + angle);
        var spawnPosition = RandomPosition();
        var positions = new List<Vector3>();
        var anglePOE = Vector3.SignedAngle(IngameManager.instance.player.position - spawnPosition, Vector3.right,
            Vector3.up);
        var left = qty / 2;
        var right = qty - left;
        for (var i = 0; i < right; i++)
        {
            var angleStep = i * angle;
            var x = spawnPosition.x + Mathf.Cos((anglePOE - angleStep) * Mathf.Deg2Rad) * radius;
            var z = spawnPosition.z + Mathf.Sin((anglePOE - angleStep) * Mathf.Deg2Rad) * radius;
            positions.Add(new Vector3(x, 0f, z));
        }

        for (var i = 1; i <= left; i++)
        {
            var angleStep = -(i * angle);
            var x = spawnPosition.x + Mathf.Cos((anglePOE - angleStep) * Mathf.Deg2Rad) * radius;
            var z = spawnPosition.z + Mathf.Sin((anglePOE - angleStep) * Mathf.Deg2Rad) * radius;
            positions.Add(new Vector3(x, 0f, z));
        }

        return positions;
    }

    bool IsEnoughtWave() { return totalWaveMax <= countWaveSpawned; }

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