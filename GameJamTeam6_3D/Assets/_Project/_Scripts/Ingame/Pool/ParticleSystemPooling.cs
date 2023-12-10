using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSystemPooling : MonoBehaviour
{
    [SerializeField] private Dictionary<int, ParticleSystem> prefabs;
    readonly Dictionary<int, Queue<ParticleSystem>> pool = new();

    public virtual void Awake()
    {
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
        var objectPool = pool[_id];
        var projectile = objectPool.Count > 0 ? objectPool.Dequeue() : Instantiate(prefabs[_id], transform);
        projectile.gameObject.SetActive(true);
        projectile.Play();
        return projectile;
    }

    public void Deactive(int _id, ParticleSystem _obj)
    {
        _obj.gameObject.SetActive(false);
        pool[_id].Enqueue(_obj);
    }








}
