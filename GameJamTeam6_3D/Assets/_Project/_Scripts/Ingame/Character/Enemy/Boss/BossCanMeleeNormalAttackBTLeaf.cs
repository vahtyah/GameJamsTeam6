using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCanMeleeNormalAttackBTLeaf : MonoBehaviour, IBehaviourTree
{
    [SerializeField] bool disabled;
    [SerializeField] EnemyDamageCollision damageCollision;

    void Awake()
    {



    }

    public bool IsDisabled()
    {
        return disabled;
    }

    public BehaviourTreeResult Tick(BehaviourTreeBossBlackboard _blackboard)
    {
        if (_blackboard.GetInfo(BehaviourTreeBlackboardInfo.NearPlayer) == false)
        {
            return BehaviourTreeResult.Fail;
        }
        _blackboard.GetAgent().GetEnemyAnimController().PlayAnim(EnemyAnimState.Attack);
        damageCollision.SetDamage(10);
        damageCollision.AttackOn();

        return BehaviourTreeResult.Sucess;
    }



}
