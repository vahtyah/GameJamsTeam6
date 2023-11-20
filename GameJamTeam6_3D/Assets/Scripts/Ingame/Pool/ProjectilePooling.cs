using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class ProjectilePooling : SerializedMonoBehaviour
{
    public static ProjectilePooling instance { get; private set; }
    [SerializeField] private Dictionary<EnemyID, GameObject> prefabs;
    private readonly Dictionary<EnemyID, Queue<GameObject>> pooledProjectiles = new();

    private void Awake()
    {
        if (!instance) instance = this;
        foreach (var keyValue in prefabs)
        {
            pooledProjectiles.Add(keyValue.Key, new Queue<GameObject>());
        }
    }

    private IEnumerator IEDestroyAfterDeactivate(float _time, Queue<GameObject> _objectPool)
    {
        yield return new WaitForSeconds(_time);
        Destroy(_objectPool.Dequeue());
    }

    public Projectile ActivateProjectile(EnemyID _id)
    {
        if (!pooledProjectiles.ContainsKey(_id)) return null;
        
        var objectPool = pooledProjectiles[_id];
        var projectile = objectPool.Count > 0 ? objectPool.Dequeue() : Instantiate(prefabs[_id], transform);
        var projectileScript = projectile.GetComponent<Projectile>();
        projectileScript.SetEnemyID(_id);
        projectile.SetActive(true);
        return projectileScript;
    }

    public void DeactivateProjectile(GameObject _projectile)
    {
        var objectPool = pooledProjectiles[_projectile.GetComponent<Projectile>().GetEnemyID()];
        _projectile.SetActive(false);
        objectPool.Enqueue(_projectile);
    }
}