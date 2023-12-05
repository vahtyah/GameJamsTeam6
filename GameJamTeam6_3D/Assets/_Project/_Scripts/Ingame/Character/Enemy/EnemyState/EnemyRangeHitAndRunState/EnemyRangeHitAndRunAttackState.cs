using UnityEngine;
using UnityEngine.AI;

namespace Ingame.Character.Enemy.EnemySpawner.EnemyState.EnemyRangeHitAndRunState
{
    public class EnemyRangeHitAndRunAttackState : EnemyRangeAttackState
    {
        private Vector3 randomPosition;
        public override EnemyAnimState OnUpdate()
        {
            attackTimer += Time.deltaTime;
            var distance = Vector3.Distance(IngameManager.instance.player.position,
                enemy.GetGameObject().transform.position);
            if (attackTimer > attackTime)
            {
                if (distance <= rangeAttack)
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
            MoveAfterAttack();
        }

        private void MoveAfterAttack()
        {
            randomPosition = GetRandomPointOnCircle(15);
            enemy.GetEnemyNav().MoveToPosition(randomPosition);
        }

        private Vector3 GetRandomPointOnCircle(float distanceAngle)
        {
            var angleAOB = Random.value > 0.5f ? distanceAngle : -distanceAngle;
            var enemyPos = enemy.GetGameObject().transform.position;
            var playerPos = IngameManager.instance.player.position;
            var angle = Vector3.SignedAngle(enemyPos - playerPos, Vector3.right, Vector3.up);
            var x = playerPos.x + rangeAttack * Mathf.Cos((angle - angleAOB) * Mathf.Deg2Rad);
            var z = playerPos.z + rangeAttack * Mathf.Sin((angle - angleAOB) * Mathf.Deg2Rad);

            return new Vector3(x, 0, z);
        }
    }
}