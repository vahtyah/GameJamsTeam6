using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourTreeInverter : MonoBehaviour, IBehaviourTree
{
    [SerializeField] bool disabled = false;
    BehaviourTreeBossBlackboard blackboard;
    IBehaviourTree onlyChild = null;

    void Start()
    {
        if (transform.GetChild(0) == null ) return;
        onlyChild = transform.GetChild(0).GetComponent<IBehaviourTree>();
    }

    public bool IsDisabled()
    {
        return disabled;
    }

    public BehaviourTreeResult Tick(BehaviourTreeBossBlackboard _blackboard)
    {
        BehaviourTreeResult result = onlyChild.Tick(_blackboard);
        if (result == BehaviourTreeResult.Running) return result;
        return result == BehaviourTreeResult.Sucess? BehaviourTreeResult.Fail : BehaviourTreeResult.Sucess;
    }
}
