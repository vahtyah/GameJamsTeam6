using System;
using UnityEngine;

public class GuidingStraightProjectile : MonoBehaviour, IProjectile
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private float lifeTime = 5f;
    
    private Transform target;
    private float timer;
    private int id;

    private void OnEnable()
    {
        timer = 0f;
    }

    private void Start()
    {
        target = IngameManager.instance.player;
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer > lifeTime)
            ProjectilePooling.instance.DeactivateProjectile(this);
        else
        {
            var dir = target.position - transform.position;
            var distanceThisFrame = speed * Time.deltaTime;
            if (dir.magnitude <= distanceThisFrame)
            {
                HitTarget();
                return;
            }
            transform.Translate(dir.normalized * distanceThisFrame, Space.World);
            // transform.LookAt(target.position);
        }
    }
    
    public GuidingStraightProjectile SetPosition(Vector3 _position)
    {
        transform.position = _position;
        return this;
    }
    
    public void SetID(int _id) => id = _id;

    public int GetID() => id;

    public GameObject GetGameObject()
    {
        return gameObject;
    }

    private void HitTarget()
    {
        ProjectilePooling.instance.DeactivateProjectile(this);
    }

    IProjectile IProjectile.SetPosition(Vector3 _pos)
    {
        transform.position = _pos;
        return this;
    }
    int damage = 5;
    public IProjectile SetDamage(int _damage)
    {
        damage = _damage;
        return this;
    }




    //Player still doesn't have collider

    // private void OnTriggerEnter(Collider other)
    // {
    //     if (other.CompareTag(/*tag*/))
    //     {
    //         ProjectilePooling.instance.DeactivateProjectile(gameObject);
    //     }
    // }
}