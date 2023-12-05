using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "EnemyData")]
public class EnemyData : SerializedScriptableObject
{
    public int damage = 5;
    public float rangeAttack = 3f;
    public int hp = 100;
    public int speed = 10;

}
