using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyNormalDieState : IEnemyState
{

    public EnemyAnimState enemyState => EnemyAnimState.Die;

    float timer = 0f;

    public void OnEnter()
    {
        ColorDebug.DebugGreen("Enter die state");
        enemy.GetEnemyNav().Die();
        enemy.GetAnim().PlayAnim(EnemyAnimState.Die);
        timer = enemy.GetAnim().GetCurrentAnimLength();
    }

    public void OnExit()
    {
        
    }

    public EnemyAnimState OnUpdate()
    {
        timer -= Time.deltaTime;
        if (timer < 0f)
        {
            enemy.GetGameObject().SetActive(false);
        }

        return enemyState;
    }

    IEnemy enemy;
    public void SetEnemy(IEnemy _enemy)
    {
        enemy = _enemy;
    }
}
