using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeleeAttack : IEnemyState
{
    [SerializeField] protected float attackTime = 2f;
    [SerializeField] EnemyDamageCollision damageCollision;
    [Header("Debug")]
    [SerializeField] protected float attackTimer = 0f;
    protected IEnemy enemy;

    public EnemyAnimState enemyState => EnemyAnimState.Attack;

    public void OnEnter()
    {
        damageCollision.SetDamage(enemy.GetEnemyData().damage);
        attackTimer = attackTime;
    }

    public void OnExit()
    {
        
    }

    public EnemyAnimState OnUpdate()
    {
        attackTimer += Time.deltaTime;
        if (attackTimer > attackTime)
        {
            if (Vector3.Distance(IngameManager.instance.player.position, enemy.GetGameObject().transform.position) < enemy.GetEnemyData().rangeAttack)
            {
                DoAttack();
                attackTimer = 0f;
            }
            else
                return EnemyAnimState.Move;
        }
        return enemyState;
    }

    void DoAttack()
    {
        enemy.GetGameObject().transform.LookAt(IngameManager.instance.player.position);
        enemy.GetEnemyNav().Attack();
        damageCollision.AttackOn();
    }

    public void SetEnemy(IEnemy _enemy)
    {
        enemy = _enemy;
    }

}
