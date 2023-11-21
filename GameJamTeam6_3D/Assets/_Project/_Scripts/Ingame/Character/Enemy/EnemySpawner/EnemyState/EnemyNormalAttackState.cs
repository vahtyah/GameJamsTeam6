using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyNormalAttackState : IEnemyState
{
    [SerializeField] protected float attackTime = 2f;
    [SerializeField] protected float attackTimer = 0f;
    public EnemyAnimState enemyState => EnemyAnimState.Attack;

    protected IEnemy enemy;

    public virtual void OnEnter()
    {
        DoAttack();
    }

    public void OnExit()
    {
        
    }

    public virtual EnemyAnimState OnUpdate()
    {

        attackTimer += Time.deltaTime;
        if (attackTimer > attackTime) {
            if (Vector3.Distance(IngameManager.instance.player.position, enemy.GetGameObject().transform.position) < 3f)
            {
                DoAttack();
            }
            else
                return EnemyAnimState.Move;
        }
        return enemyState;
    }

    protected virtual void DoAttack()
    {
        enemy.GetGameObject().transform.LookAt(IngameManager.instance.player.position);
        attackTimer = 0f;
        enemy.GetEnemyNav().Attack();
    }


    public void SetEnemy(IEnemy _enemy)
    {
        enemy = _enemy;
    }
}
