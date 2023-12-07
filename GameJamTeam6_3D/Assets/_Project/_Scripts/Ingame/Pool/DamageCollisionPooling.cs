using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageCollisionPooling : SerializedMonoBehaviour
{
    public static DamageCollisionPooling instance { get; private set; }
    [SerializeField] Dictionary<int, GameObject> prefabs;
    private readonly Dictionary<int, Queue<GameObject>> pool = new();
    private void Awake()
    {
        if (!instance) instance = this;
        foreach (var keyValue in prefabs)
        {
            pool.Add(keyValue.Key, new Queue<GameObject>());
            for (int i = 0; i < 5; i++)
            {
                GameObject obj = Instantiate(prefabs[keyValue.Key], transform);
                obj.gameObject.SetActive(false);
                pool[keyValue.Key].Enqueue(obj);
            }
        }
    }

    public GameObject ActivateProjectile(int _id)
    {
        var projectile = pool[_id].Count > 0 ? pool[_id].Dequeue() : Instantiate(prefabs[_id], transform);
        projectile.SetActive(true);
        return projectile;
    }

    public void DeactivateProjectile(GameObject _obj, int _id)
    {
        _obj.SetActive(false);
        pool[_id].Enqueue(_obj);
    }
}
