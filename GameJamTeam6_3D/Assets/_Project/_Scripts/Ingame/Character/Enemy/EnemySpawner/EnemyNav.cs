using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyNav 
{
    NavMeshAgent agent;
    EnemyAnimController animController;

    float stableSpeed = -1;

    public EnemyNav SetAgent(NavMeshAgent _agent)
    {
        agent = _agent;
        agent.enabled = true;
        return this;
    }

    public EnemyNav SetSpeed(float _speed)
    {
        stableSpeed = _speed;
        return this;
    }

    public EnemyNav SetAnimController(EnemyAnimController _anim)
    {
        animController = _anim;
        return this;
    }

    public void Move()
    {
        animController.PlayAnim(EnemyAnimState.Move);
        agent.speed = stableSpeed;
        agent.SetDestination(IngameManager.instance.player.position);
    }

    public void MoveToPosition(Vector3 position)
    {
        agent.velocity = Vector3.zero;
        animController.PlayAnim(EnemyAnimState.Move);
        agent.speed = stableSpeed;
        agent.SetDestination(position);
    }

    public void Idle()
    {
        animController.PlayAnim(EnemyAnimState.Idle);
        Stop();
    }

    public void Attack()
    {
        Stop();
        animController.PlayAnim(EnemyAnimState.Attack);
    }

    public void AttackWithSlip(float slipFactor)
    {
        Slip(slipFactor);
        animController.PlayAnim(EnemyAnimState.Attack);
    }

    public void Die()
    {
        Stop();
        animController.PlayAnim(EnemyAnimState.Die);
    }
    void Stop()
    {
        agent.speed = 0;
        if (agent.destination == agent.gameObject.transform.position) return;
        agent.destination = agent.gameObject.transform.position;
    }

    private void Slip(float slipFactor)
    {
        agent.speed = 0;
        agent.velocity *= slipFactor;
    }
}
