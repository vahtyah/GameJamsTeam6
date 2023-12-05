using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyNormalMoveState : IEnemyState
{
    [SerializeField] protected float rangeAttack = 6f;
    public EnemyAnimState enemyState => EnemyAnimState.Move;

    protected IEnemy enemy;

    public void OnEnter()
    {

    }

    public void OnExit()
    {
        
    }

    public EnemyAnimState OnUpdate()
    {
        if (Vector3.Distance( IngameManager.instance.player.position, enemy.GetGameObject().transform.position) < rangeAttack)
        {
            return EnemyAnimState.Attack;
        }
        enemy.GetGameObject().transform.LookAt(IngameManager.instance.player.position);
        enemy.GetEnemyNav().MoveToPlayer();
        return enemyState;
    }

    public void SetEnemy(IEnemy _enemy)
    {
        enemy = _enemy;
    }

    public float GetRangeAttack => rangeAttack;
}
