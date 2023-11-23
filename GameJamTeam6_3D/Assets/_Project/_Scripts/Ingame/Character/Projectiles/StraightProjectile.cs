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

    void Update()
    {
        timer += Time.deltaTime;
        rb.velocity = Vector3.forward * speed;
        if (timer > timeExist)
        {
            ProjectilePooling.instance.DeactivateProjectile(this);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(GlobalString.tagPlayer))
        {
            Player.instance.AddHealth(- damage);
        }   
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

    public IProjectile SetPosition(Vector3 _pos)
    {
        transform.position = _pos;
        return this;
    }

    public IProjectile SetDamage(int _damage)
    {
        damage = _damage;
        return this;
    }
}
