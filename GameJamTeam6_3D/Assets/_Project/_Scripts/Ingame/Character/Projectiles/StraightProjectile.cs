using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StraightProjectile : MonoBehaviour, IProjectile
{
    [SerializeField] Rigidbody rb;
    [SerializeField] int speed = 7;
    int id = 0;
    float timeExist = 5f;
    float timer = 0;
    int damage = 5;
    [SerializeField] bool isPlayer;
    void OnEnable()
    {
        timer = 0;
    }

    void Update()
    {
        timer += Time.deltaTime;
        rb.velocity = transform.forward * speed;
        if (timer > timeExist)
        {
            ProjectilePooling.instance.DeactivateProjectile(this);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        bool pop = false;
        if (other.gameObject.CompareTag(GlobalInfo.obstacleTagAndLayer))
        {
            pop = true;
        }
        if (isPlayer == false)
        {
            if (other.gameObject.CompareTag(GlobalInfo.tagPlayer))
            {
                Player.instance.AddHealth(-damage);
                pop = true;
            }
        }
        else if (other.gameObject.CompareTag(GlobalInfo.enemyTagAndLayer))
        {
                other.GetComponent<IEnemy>().AddHealth(-damage);
                pop = true;
        }
        if (pop == false) return;
        ProjectileImpactPooling.instance.Activate(id).gameObject.transform.position = transform.position;
        ProjectilePooling.instance.DeactivateProjectile(this);
    }

    public GameObject GetGameObject()
    {
        return gameObject;
    }

    public int GetID()
    {
        return id;
    }

    public void SetID(int _id)
    {
        id = _id;
    }

    public IProjectile SetDamage(int _damage)
    {
        damage = _damage;
        return this;
    }

    public IProjectile SetPossession(bool _isPlayer)
    {
        isPlayer = _isPlayer;
        return this;
    }
}
