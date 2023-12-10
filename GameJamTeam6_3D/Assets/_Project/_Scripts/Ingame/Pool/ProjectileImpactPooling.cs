using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ProjectileImpactPooling : SerializedMonoBehaviour
{
    public static ProjectileImpactPooling instance { get; private set; }
    [SerializeField] private Dictionary<int, ParticleSystem> prefabs;
    //readonly Dictionary<int, Queue<ParticleSystem>> pool = new();
    Dictionary<int, List<ParticleSystem>> pooling = new Dictionary<int, List<ParticleSystem>>();

    private void Awake()
    {
        if (!instance) instance = this;
        foreach (var keyValue in prefabs)
        {
            
            pooling[keyValue.Key] = new List<ParticleSystem>();
            for (int i = 0; i < 5; i++)
            {
                ParticleSystem obj = Instantiate(prefabs[keyValue.Key], transform);
                obj.gameObject.SetActive(false);
                
                pooling[keyValue.Key].Add(obj);
            }
        }
    }

    public ParticleSystem Activate(int _id)
    {
        for (int i = 0; i < pooling[_id].Count; i++)
        {
            if (pooling[_id][i].gameObject.activeSelf) continue;
            pooling[_id][i].gameObject.SetActive(true);
            pooling[_id][i].Play();
            return pooling[_id][i];
        }
        var projectile =Instantiate(prefabs[_id], transform);
        projectile.gameObject.SetActive(true);
        projectile.Play();
        pooling[_id].Add(projectile);
        return projectile;
    }

    //public void Deactive(int _id, ParticleSystem _obj)
    //{
    //    _obj.gameObject.SetActive(false);
    //    pool[_id].Enqueue(_obj);
    //}

}
