using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IProjectile 
{
    public void SetID(int _id);
    public IProjectile SetPossession(bool _isPlayer);
    public int GetID();

    public GameObject GetGameObject();
    public IProjectile SetDamage(int _damage);

}
