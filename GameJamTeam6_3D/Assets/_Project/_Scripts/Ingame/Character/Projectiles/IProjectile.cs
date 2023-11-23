using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IProjectile 
{
    public void SetID(int _id);
    public int GetID();

    public GameObject GetGameObject();

    public IProjectile SetPosition(Vector3 _pos);
    public IProjectile SetDamage(int _damage);

}
