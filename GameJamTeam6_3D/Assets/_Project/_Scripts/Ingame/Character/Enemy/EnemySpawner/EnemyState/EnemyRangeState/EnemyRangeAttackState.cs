using System.Buffers.Text;
using UnityEngine;

public class EnemyRangeAttackState : EnemyNormalAttackState
{
    private EnemyID enemyID;
    protected float rangeAttack;
    public override void OnEnter()
    {
        base.OnEnter();
        enemyID = LevelConfig.instance.GetCurrentLevel().GetWaveData(enemy.GetWave()).enemyId;
        rangeAttack = ((EnemyNormalMoveState)enemy.GetEnemyStateHandler().GetState(EnemyAnimState.Move)).GetRangeAttack;
        Debug.Log(rangeAttack);
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
        var projectile = ProjectilePooling.instance.ActivateProjectile(enemyID);
        if (projectile != null) projectile.SetPosition(enemyGo.transform.position);
    }
}