using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCanNormalMoveBTLeaf : MonoBehaviour, IBehaviourTree
{
    [SerializeField] bool disabled;

    public bool IsDisabled()
    {
        return disabled;
    }

    public BehaviourTreeResult Tick(BehaviourTreeBlackboard _blackboard)
    {
        if (_blackboard.GetInfo(BehaviourTreeBlackboardInfo.NearPlayer))
            return BehaviourTreeResult.Fail;
        _blackboard.Agent.GetEnemyNav().MoveToPlayer();
        return BehaviourTreeResult.Sucess;
    }
}
