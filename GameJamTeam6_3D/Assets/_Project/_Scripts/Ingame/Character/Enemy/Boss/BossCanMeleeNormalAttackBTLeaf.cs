using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCanMeleeNormalAttackBTLeaf : MonoBehaviour, IBehaviourTree
{
    [SerializeField] bool disabled;
    [SerializeField] EnemyDamageCollision damageCollision;
    bool isAttacking = false;
    bool attackSuccess = false;

    void Awake()
    {
        damageCollision.complete = OnComplete;

    }

    void OnComplete()
    {
        attackSuccess = true;
    }

    public bool IsDisabled()
    {
        return disabled;
    }

    public BehaviourTreeResult Tick(BehaviourTreeBossBlackboard _blackboard)
    {
        if (attackSuccess)
        {
            attackSuccess = false;
            isAttacking = false;
            return BehaviourTreeResult.Sucess;
        }
        if (_blackboard.GetInfo(BehaviourTreeBlackboardInfo.NearPlayer) == false && isAttacking == false)
        {
            return BehaviourTreeResult.Fail;
        }
        if (isAttacking == false)
        {
            isAttacking = true;
            _blackboard.GetAgent().GetEnemyAnimController().PlayAnim(EnemyAnimState.Attack);
            damageCollision.SetDamage(10);
            damageCollision.AttackOn();
        }
        return BehaviourTreeResult.Running;
    }



}
