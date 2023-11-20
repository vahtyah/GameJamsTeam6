using System;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private float lifeTime = 5f;
    
    private Transform target;
    private float timer;
    private EnemyID enemyID;

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
        if(!gameObject.activeSelf) return;
        
        timer += Time.deltaTime;
        if (timer > lifeTime)
            ProjectilePooling.instance.DeactivateProjectile(gameObject);
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
    
    public Projectile SetPosition(Vector3 _position)
    {
        transform.position = _position;
        return this;
    }
    
    public void SetEnemyID(EnemyID _id) => enemyID = _id;

    public EnemyID GetEnemyID() => enemyID;

    private void HitTarget()
    {
        ProjectilePooling.instance.DeactivateProjectile(gameObject);
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