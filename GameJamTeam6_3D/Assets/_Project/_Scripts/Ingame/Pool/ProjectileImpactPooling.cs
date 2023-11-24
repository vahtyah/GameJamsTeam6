using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileImpactPooling : SerializedMonoBehaviour
{
    public static ProjectileImpactPooling instance { get; private set; }
    [SerializeField] private Dictionary<int, ParticleSystem> prefabs;
    readonly Dictionary<int, Queue<ParticleSystem>> pool = new();

    private void Awake()
    {
        if (!instance) instance = this;
        foreach (var keyValue in prefabs)
        {
            pool.Add(keyValue.Key, new Queue<ParticleSystem>());
            for (int i = 0; i < 5; i++)
            {
                ParticleSystem obj = Instantiate(prefabs[keyValue.Key], transform);
                obj.gameObject.SetActive(false);
                pool[keyValue.Key].Enqueue(obj);
            }
        }
    }

    public ParticleSystem Activate(int _id)
    {
        ParticleSystem target = null;
        var objectPool = pool[_id];
        var projectile = objectPool.Count > 0 ? objectPool.Dequeue() : Instantiate(prefabs[_id], transform);
        projectile.gameObject.SetActive(true);
        projectile.Play();
        return target;
    }

    public void Deactive(int _id, ParticleSystem _obj)
    {
        _obj.gameObject.SetActive(false);
        pool[_id].Enqueue(_obj);
    }

}
