using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyNormalDieState : IEnemyState
{
    public EnemyAnimState enemyState => EnemyAnimState.Die;

    public void OnEnter()
    {
        
    }

    public void OnExit()
    {
        
    }

    public EnemyAnimState OnUpdate()
    {
        return enemyState;
    }

    public void SetEnemy(IEnemy _enemy)
    {
       
    }
}
