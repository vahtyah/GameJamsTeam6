using System;
using _Project._Scripts.Ingame.Manager;
using UnityEngine;

public class MissileProjectile : MonoBehaviour, IProjectile
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private int speed = 3;
    [SerializeField] private float explosionRadius = 3f;
    private int id = 0;
    private float timeExits = 5f;
    private float timer = 5;
    private int damage = 3;

    private void OnEnable() { timer = 0; }

    private void Update()
    {
        timer += Time.deltaTime;
        rb.velocity = transform.forward * speed;
        if (timer > timeExits)
            ProjectilePooling.instance.DeactivateProjectile(this);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(GlobalInfo.enemyTagAndLayer))
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
            foreach (Collider collider in colliders)
            {
                if (collider.gameObject.CompareTag(GlobalInfo.enemyTagAndLayer))
                {
                    collider.GetComponent<IEnemy>().AddHealth(-damage);
                    SoundManager.Instance.PlaySFX(Sound.HitEnemy);
                }
            }
        }

        ProjectileImpactPooling.instance.Activate(id).gameObject.transform.position = transform.position;
        ProjectilePooling.instance.DeactivateProjectile(this);
    }

    public void SetID(int _id) => id = _id;

    public IProjectile SetPossession(bool _isPlayer) { return this; }

    public int GetID() => id;

    public GameObject GetGameObject() => gameObject;

    public IProjectile SetDamage(int _damage)
    {
        damage = _damage;
        return this;
    }
}