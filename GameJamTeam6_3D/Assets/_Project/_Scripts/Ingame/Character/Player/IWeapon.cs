using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWeapon 
{


    public void DoAttack();

    public int GetDamage();

    public bool CanAttack();

    public GameObject GetObject();



}
