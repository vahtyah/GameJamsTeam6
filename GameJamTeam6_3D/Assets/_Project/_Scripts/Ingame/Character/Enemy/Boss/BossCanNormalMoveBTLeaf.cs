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

    public BehaviourTreeResult Tick(BehaviourTreeBossBlackboard _blackboard)
    {
        if (_blackboard.GetInfo(BehaviourTreeBlackboardInfo.NearPlayer))
            return BehaviourTreeResult.Fail;
        _blackboard.GetAgent().GetEnemyNav().MoveToPlayer();
        return BehaviourTreeResult.Sucess;
    }
}
