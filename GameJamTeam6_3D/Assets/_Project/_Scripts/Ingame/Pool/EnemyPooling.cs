using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyPooling : SerializedMonoBehaviour
{
    public static EnemyPooling instance;

    [SerializeField] Dictionary<EnemyID, IEnemy> prefabs;
    Dictionary<EnemyID, List<IEnemy>> pooledEnemies = new Dictionary<EnemyID, List<IEnemy>>();


    private void Awake()
    {
        if (instance == null) instance = this;
        for (int i = 0; i < Enum.GetNames(typeof(EnemyID)).Length; i++)
        {
            pooledEnemies.Add((EnemyID)i, new List<IEnemy>());
        }
    }

    public IEnemy SpawnEnemy(EnemyID _id, Vector3 _pos)
    {
        for (int i = 0; i < pooledEnemies[_id].Count; i++)
        {
            if (pooledEnemies[_id][i].GetGameObject().activeSelf) continue;
            pooledEnemies[_id][i].Setup();
            pooledEnemies[_id][i].GetGameObject().transform.position = _pos;
            pooledEnemies[_id][i].GetGameObject().SetActive(true);
            return pooledEnemies[_id][i];
        }
        GameObject newObj = Instantiate(prefabs[_id].GetGameObject(), transform);
        IEnemy newEnemy = newObj.GetComponent<IEnemy>();
        newEnemy.Setup();
        newEnemy.GetGameObject().transform.position = _pos;
        newEnemy.GetGameObject().SetActive(true);
        pooledEnemies[_id].Add(newEnemy);
        return newEnemy;
    }

    public IEnemy GetIEnemy(EnemyID enemyID)
    {
        return prefabs.ContainsKey(enemyID) ? prefabs[enemyID] : null;
    }
}


