using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyNav 
{
    NavMeshAgent agent;
    EnemyAnimController animController;
    private NavMeshPath navMeshPath;
    float stableSpeed = -1;

    public EnemyNav SetAgent(NavMeshAgent _agent)
    {
        agent = _agent;
        navMeshPath = new NavMeshPath();
        agent.enabled = false;
        return this;
    }

    public EnemyNav SetSpeed(float _speed)
    {
        stableSpeed = _speed;
        agent.enabled = false;
        return this;
    }

    public void StartAgent()
    {
        agent.enabled = true;
    }

    public EnemyNav SetAnimController(EnemyAnimController _anim)
    {
        animController = _anim;
        return this;
    }
    
    public void MoveToPlayer()
    {
        agent.enabled = true;
        animController.PlayAnim(EnemyAnimState.Move);
        agent.speed = stableSpeed;
        agent.CalculatePath(IngameManager.instance.player.position, navMeshPath);
        if (CheckMove())
        {
            agent.SetDestination(IngameManager.instance.player.position);
        }
    }

    public void MoveToPosition(Vector3 position)
    {
        agent.enabled = true;
        agent.velocity = Vector3.zero;
        animController.PlayAnim(EnemyAnimState.Move);
        agent.speed = stableSpeed;
        agent.SetDestination(position);
        CheckMove();
    }

    bool CheckMove()
    {
        //return;
        if (navMeshPath.status != NavMeshPathStatus.PathComplete)
        {
            animController.PlayAnim(EnemyAnimState.Idle);
            agent.destination = agent.gameObject.transform.position;
            Stop();
            return false;
        }
        return true;
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
        agent.velocity = Vector3.zero;
        agent.enabled = false;
    }



    private void Slip(float slipFactor)
    {
        agent.speed = 0;
        agent.velocity *= slipFactor;
    }
}
