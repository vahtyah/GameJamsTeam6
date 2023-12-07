using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBehaviourTree
{
    //public void Setup();
    //public void SetBlackboard(BehaviourTreeBlackboard _blackboard);

    public BehaviourTreeResult Tick(BehaviourTreeBossBlackboard _blackboard);
    public bool IsDisabled();

}

public enum BehaviourTreeResult
{
    Sucess, Running, Fail
}
