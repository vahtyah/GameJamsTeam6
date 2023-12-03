using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GachaReward : MonoBehaviour
{
    [Range(0f, 1f)]
    public float rate;
    public GachaType type;

    public void DoGacha()
    {

    }

}

public enum GachaType
{
    WeaponItem, Coin
}

