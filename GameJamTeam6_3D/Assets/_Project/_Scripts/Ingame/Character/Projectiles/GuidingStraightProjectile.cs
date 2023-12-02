using System;
using UnityEngine;

public class GuidingStraightProjectile : MonoBehaviour, IProjectile
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private float lifeTime = 5f;
    
    private Transform target;
    private float timer;
    private int id;
    bool isPlayer;
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
    int damage = 5;
    public IProjectile SetDamage(int _damage)
    {
        damage = _damage;
        return this;
    }

    private void OnTriggerEnter(Collider other)
    {
        bool pop = false;
        if (other.gameObject.CompareTag(GlobalString.obstacleTagAndLayer))
        {
            ProjectilePooling.instance.DeactivateProjectile(this);
            pop = true;
            return;
        }
        if ( isPlayer == false)
        {
            if (other.gameObject.CompareTag(GlobalString.tagPlayer))
            {
                ProjectilePooling.instance.DeactivateProjectile(this);
                Player.instance.AddHealth(-damage);
                pop = true;
            }
        }
        else
        {
            if (other.gameObject.CompareTag(GlobalString.enemyTagAndLayer))
            {
                ProjectilePooling.instance.DeactivateProjectile(this);
                other.GetComponent<IEnemy>().AddHealth(-damage);
                pop = true;
            }
        }
        if (pop == false) return;
        ProjectileImpactPooling.instance.Activate(id).gameObject.transform.position = transform.position;
    }
    public IProjectile SetPossession(bool _isPlayer)
    {
        isPlayer = _isPlayer;
        return this;
    }
}