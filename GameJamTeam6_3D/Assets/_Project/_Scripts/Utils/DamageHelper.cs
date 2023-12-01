using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageHelper 
{
    public static int GetPlayerDamage(int _baseDamage)
    {
        int damage = _baseDamage;
        if (CanCrit())
        {
            damage += (damage * Player.instance.GetPlayerData().critDamgPercent.value)/100;
        }
        return damage;
    }

    static bool CanCrit()
    {
        int ran = Random.Range(1, 101);
        return ran < Player.instance.GetPlayerData().critChance.value ;
    }


}
