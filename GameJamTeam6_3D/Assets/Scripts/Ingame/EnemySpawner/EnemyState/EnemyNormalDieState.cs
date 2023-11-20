using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyNormalDieState : IEnemyState
{
    public EnemyState enemyState => EnemyState.Die;

    public void OnEnter()
    {
        
    }

    public void OnExit()
    {
        
    }

    public EnemyState OnUpdate()
    {
        return enemyState;
    }

    public void SetEnemy(IEnemy _enemy)
    {
       
    }
}
