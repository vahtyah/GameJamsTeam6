using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourTreeBTCheckInfo : MonoBehaviour, IBehaviourTree
{
    [SerializeField] bool disabled;
    [SerializeField] BehaviourTreeBlackboardInfo info;
    BehaviourTreeBossBlackboard blackboard;

    public bool IsDisabled()
    {
        return disabled;
    }


    public BehaviourTreeResult Tick(BehaviourTreeBossBlackboard _blackboard)
    {
        return blackboard.GetInfo(info)? BehaviourTreeResult.Sucess : BehaviourTreeResult.Fail;
    }

}
