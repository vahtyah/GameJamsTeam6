using System.Buffers.Text;

public class EnemyRangeAttackState : EnemyNormalAttackState
{
    private EnemyID enemyID;
    public override void OnEnter()
    {
        base.OnEnter();
        enemyID = LevelConfig.instance.GetCurrentLevel().GetWaveData(enemy.GetWave()).enemyId;
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
        //Do damage
        // IngameManager.instance.player.GetComponent<CharacterHealth>().AddHealth(-enemy.GetDamage());
    }
}