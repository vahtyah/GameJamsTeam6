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
    [SerializeField] Dictionary<int, int> enemyWaveRecord = new Dictionary<int, int>();
    List<IEnemy> enemySpawned = new List<IEnemy>();
    List<int> wavesActive = new List<int>();
    int totalWaveMax;
    int countWaveSpawned = 0;

    private void Awake()
    {
        if (instance == null) instance = this;
    }

    public void StartSetup()
    {
        totalWaveMax = MapScene.instance.GetSpawnData().GetTotalWaveMax();
        spawnMarkers = MapSceneManager.instance.GetCurrentMap().GetSpawnPoints();
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
            int ranEnemyWaveType = UnityEngine.Random.Range(0, MapScene.instance.GetSpawnData().GetTotalAvailableWaves());
            int totalEnemiesInWave = MapScene.instance.GetSpawnData().GetWaveData(ranEnemyWaveType).amount;
            EnemyID currentEnemyID = MapScene.instance.GetSpawnData().GetWaveData(ranEnemyWaveType).enemyId;
            enemyWaveRecord.Add(waveId, totalEnemiesInWave);
            for (int j = 0; j < totalEnemiesInWave; j++)
            {
                IEnemy enemy = EnemyPooling.instance.SpawnEnemy(currentEnemyID);
                enemySpawned.Add(enemy);
                enemy.SetThisEnemyFromWave(waveId)
                    .GetGameObject().transform.position = RandomPosition()
                    ;
                yield return new WaitForSeconds(MapScene.instance.GetSpawnData().GetWaitTimeWave());
            }
            countWaveSpawned++;
        }
    }
    List<Vector3> tempPosisitions = new List<Vector3>();
    public IEnumerator IESpawnArcShape(Vector3 _pos)
    {
        yield return null;
        int ranEnemyWaveType =
            UnityEngine.Random.Range(0, MapScene.instance.GetSpawnData().GetTotalAvailableWaves());
        int totalEnemiesInWave = MapScene.instance.GetSpawnData().GetWaveData(ranEnemyWaveType).amount;
        EnemyID currentEnemyID = MapScene.instance.GetSpawnData().GetWaveData(ranEnemyWaveType).enemyId;
        var IEnemy = EnemyPooling.instance.GetIEnemy(currentEnemyID);
        tempPosisitions = GetArcShapePositions(totalEnemiesInWave, IEnemy, _pos);
        for (int j = 0; j < totalEnemiesInWave; j++)
        {
            var enemy = EnemyPooling.instance.SpawnEnemy(currentEnemyID);
            enemySpawned.Add(enemy);
            enemy.SetThisEnemyFromWave(-1)
                .GetGameObject().transform.position = tempPosisitions[j];
        }
    }

    public IEnumerator IESpawnGroup(Vector3 _pos)
    {
        yield return null;
        int ranEnemyWaveType =
            UnityEngine.Random.Range(0, MapScene.instance.GetSpawnData().GetTotalAvailableWaves());
        int totalEnemiesInWave = MapScene.instance.GetSpawnData().GetWaveData(ranEnemyWaveType).amount;
        EnemyID currentEnemyID = MapScene.instance.GetSpawnData().GetWaveData(ranEnemyWaveType).enemyId;
        var IEnemy = EnemyPooling.instance.GetIEnemy(currentEnemyID);
        tempPosisitions = GetGroupPosition(totalEnemiesInWave, IEnemy, _pos);
        for (int j = 0; j < totalEnemiesInWave; j++)
        {
            var enemy = EnemyPooling.instance.SpawnEnemy(currentEnemyID);
            enemySpawned.Add(enemy);
            enemy.SetThisEnemyFromWave(-1)
                .GetGameObject().transform.position = tempPosisitions[j];
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

    List<Vector3> GetGroupPosition(int _qty, IEnemy enemy, Vector3 _spawnPosition)
    {
        return FindGroupPosition(_qty, enemy, _spawnPosition);
    }

    List<Vector3> FindGroupPosition(int _qty, IEnemy enemy, Vector3 _spawnPosition)
    {
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
                positions.Add(_spawnPosition + size * Vector3.right * i - cols * size * Vector3.forward);
            }
            cols++;
        }
        return positions;
    }
    List<Vector3> GetArcShapePositions(int _qty, IEnemy enemy, Vector3 _spawnPosition)
    {
        return FindArcShapePositions(_qty, enemy, _spawnPosition);
    }

    List<Vector3> FindArcShapePositions(int qty, IEnemy enemy, Vector3 _spawnPos)
    {
        BoxCollider collider = enemy.GetGameObject().GetComponent<BoxCollider>();
        float size = Mathf.Max(collider.size.x, collider.size.z);
        var radius = 2 * size;
        var angle = Mathf.Acos((radius * radius * 2 - size * size) / (2 * radius * radius)) * Mathf.Rad2Deg;
        while (angle * qty > 360f)
        {
            radius += size;
            angle = Mathf.Acos((radius * radius * 2 - size * size) / (2 * radius * radius)) * Mathf.Rad2Deg;
        }
        print(radius + " " + angle);
        var positions = new List<Vector3>();
        var anglePOE = Vector3.SignedAngle(IngameManager.instance.player.position - _spawnPos, Vector3.right,
            Vector3.up);
        var left = qty / 2;
        var right = qty - left;
        for (var i = 0; i < right; i++)
        {
            var angleStep = i * angle;
            var x = _spawnPos.x + Mathf.Cos((anglePOE - angleStep) * Mathf.Deg2Rad) * radius;
            var z = _spawnPos.z + Mathf.Sin((anglePOE - angleStep) * Mathf.Deg2Rad) * radius;
            positions.Add(new Vector3(x, 0f, z));
        }
        for (var i = 1; i <= left; i++)
        {
            var angleStep = -(i * angle);
            var x = _spawnPos.x + Mathf.Cos((anglePOE - angleStep) * Mathf.Deg2Rad) * radius;
            var z = _spawnPos.z + Mathf.Sin((anglePOE - angleStep) * Mathf.Deg2Rad) * radius;
            positions.Add(new Vector3(x, 0f, z));
        }
        return positions;
    }

    bool IsEnoughtWave() { return totalWaveMax <= countWaveSpawned; }

    public void OnEnemyDie(int _atWave)
    {
        if (_atWave <= -1) return;
        enemyWaveRecord[_atWave]--;
        if (enemyWaveRecord[_atWave] == 0)
        {
            countWaveSpawned--;
            StartSpawning();
        }
    }
}

public enum EnemySpawnType
{
    Single, Arc, Group
}
