using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Sirenix.OdinInspector;
using UnityEngine;

public class ProjectilePooling : SerializedMonoBehaviour
{
    public static ProjectilePooling instance { get; private set; }
    [SerializeField] private Dictionary<int, IProjectile> prefabs;
    private readonly Dictionary<int, Queue<GameObject>> pooledProjectiles = new();

    private void Awake()
    {
        if (!instance) instance = this;
        foreach (var keyValue in prefabs)
        {
            pooledProjectiles.Add(keyValue.Key, new Queue<GameObject>());
            for (int i = 0; i < 5; i++)
            {
                GameObject obj = Instantiate(prefabs[keyValue.Key].GetGameObject(), transform);
                obj.gameObject.SetActive(false);
                pooledProjectiles[keyValue.Key].Enqueue(obj);
            }
        }
    }

    public IProjectile ActivateProjectile(int _id)
    {
        if (!pooledProjectiles.ContainsKey(_id)) return null;
        
        var objectPool = pooledProjectiles[_id];
        var projectile = objectPool.Count > 0 ? objectPool.Dequeue() : Instantiate(prefabs[_id].GetGameObject(), transform);
        var projectileScript = projectile.GetComponent<IProjectile>();
        projectileScript.SetID(_id);
        projectile.SetActive(true);
        return projectileScript;
    }

    public void DeactivateProjectile(IProjectile _projectile)
    {
        var objectPool = pooledProjectiles[_projectile.GetID()];
        _projectile.GetGameObject().SetActive(false);
        objectPool.Enqueue(_projectile.GetGameObject());
    }
}