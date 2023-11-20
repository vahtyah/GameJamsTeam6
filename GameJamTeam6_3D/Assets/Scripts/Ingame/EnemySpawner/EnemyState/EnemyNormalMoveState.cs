using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyNormalMoveState : IEnemyState
{
    [SerializeField] float rangeAttack = 6f;
    public EnemyState enemyState => EnemyState.Move;

    IEnemy enemy;

    public void OnEnter()
    {

    }

    public void OnExit()
    {
        
    }

    public EnemyState OnUpdate()
    {
        //ColorDebug.DebugGreen(enemy.GetGameObject().name = Vector3.Distance(IngameManager.instance.player.position, enemy.GetGameObject().transform.position).ToString());
        //ColorDebug.DebugYellow( Vector3.Distance(IngameManager.instance.player.position, enemy.GetGameObject().transform.position) < rangeAttack);
        //ColorDebug.DebugRed(rangeAttack);
        if (Vector3.Distance( IngameManager.instance.player.position, enemy.GetGameObject().transform.position) < rangeAttack)
        {
            return EnemyState.Attack;
        }
        enemy.GetGameObject().transform.LookAt(IngameManager.instance.player.position);
        enemy.GetEnemyNav().Move();
        return enemyState;
    }

    public void SetEnemy(IEnemy _enemy)
    {
        enemy = _enemy;
    }

}
