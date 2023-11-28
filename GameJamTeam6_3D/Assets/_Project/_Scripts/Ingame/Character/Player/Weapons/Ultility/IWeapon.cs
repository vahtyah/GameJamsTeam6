using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWeapon 
{
    public void Setup();

    public void Shoot();

    public int GetDamage();

    public bool CanAttack();

    public GameObject GetGameObject();



}
