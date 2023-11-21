using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyNormalIdleState :  IEnemyState
{
    [SerializeField] float waitTimer = 1f;
    float timer = 0;
    public EnemyState enemyState => EnemyState.Idle;
    IEnemy enemy;


    public void OnEnter()
    {
        timer = 0;
        enemy.GetEnemyNav().Idle();
    }

    public void OnExit()
    {
        
    }

    public EnemyState OnUpdate()
    {
        timer += Time.deltaTime;
        if (timer > waitTimer)
        {
            return EnemyState.Move;
        }
        return enemyState;
    }

    public void SetEnemy(IEnemy _enemy)
    {
        enemy = _enemy;
    }
}
