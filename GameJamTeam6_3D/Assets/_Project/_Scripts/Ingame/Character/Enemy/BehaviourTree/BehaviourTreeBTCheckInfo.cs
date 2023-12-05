using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourTreeBTCheckInfo : MonoBehaviour, IBehaviourTree
{
    [SerializeField] bool disabled;
    [SerializeField] BehaviourTreeBlackboardInfo info;
    BehaviourTreeBlackboard blackboard;

    public bool IsDisabled()
    {
        return disabled;
    }


    public BehaviourTreeResult Tick(BehaviourTreeBlackboard _blackboard)
    {
        return blackboard.GetInfo(info)? BehaviourTreeResult.Sucess : BehaviourTreeResult.Fail;
    }

}
