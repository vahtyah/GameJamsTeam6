using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyNormalDieState : IEnemyState
{

    public EnemyAnimState enemyState => EnemyAnimState.Die;

    public void OnEnter()
    {
        enemy.GetEnemyNav().Die();
        enemy.OnDie();
    }

    public void OnExit()
    {
        
    }

    public EnemyAnimState OnUpdate()
    {
        return enemyState;
    }

    IEnemy enemy;
    public void SetEnemy(IEnemy _enemy)
    {
        enemy = _enemy;
    }
}
