using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyNormalIdleState :  IEnemyState
{
    [SerializeField] float waitTimer = 1f;
    float timer = 0;
    public EnemyAnimState enemyState => EnemyAnimState.Idle;
    IEnemy enemy;


    public void OnEnter()
    {
        timer = 0;
        enemy.GetEnemyNav().Idle();
    }

    public void OnExit()
    {
        
    }

    public EnemyAnimState OnUpdate()
    {
        timer += Time.deltaTime;
        if (timer > waitTimer)
        {
            return EnemyAnimState.Move;
        }
        return enemyState;
    }

    public void SetEnemy(IEnemy _enemy)
    {
        enemy = _enemy;
    }
}
