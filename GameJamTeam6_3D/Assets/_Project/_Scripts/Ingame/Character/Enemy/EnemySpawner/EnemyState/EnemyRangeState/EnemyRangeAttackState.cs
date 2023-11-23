﻿using System.Buffers.Text;
using UnityEngine;

public class EnemyRangeAttackState : EnemyNormalAttackState
{
    //private EnemyID enemyID;
    protected float rangeAttack;
    public override void OnEnter()
    {
        //enemyID = LevelConfig.instance.GetCurrentLevel().GetWaveData(enemy.GetWave()).enemyId;
        rangeAttack = enemy.GetEnemyData().rangeAttack;
        base.OnEnter();
    }

    public override EnemyAnimState OnUpdate()
    { 
        attackTimer += Time.deltaTime;
        if (attackTimer > attackTime) {
            if (Vector3.Distance(IngameManager.instance.player.position, enemy.GetGameObject().transform.position) <= rangeAttack)
            {
                DoAttack();
            }
            else
                return EnemyAnimState.Move;
        }
        return enemyState;
    }

    protected override void DoAttack()
    {
        base.DoAttack();
        DoDamage();
    }

    private void DoDamage()
    {
        var enemyGo = enemy.GetGameObject();
        //Activate projectile
        var projectile = ProjectilePooling.instance.ActivateProjectile(0).SetDamage(enemy.GetEnemyData().damage)
            .SetPosition((enemy as IRangeEnemy).GetShootTransform().position)
            ;
        if (projectile != null) projectile.SetPosition(enemyGo.transform.position);
    }
}