using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourTreeInverter : SerializedMonoBehaviour, IBehaviourTree
{
    [SerializeField] bool disabled = false;
    BehaviourTreeBossBlackboard blackboard;
    [SerializeField] IBehaviourTree onlyChild = null;

    void Start()
    {
        //if (transform.GetChild(0) == null ) return;
        //onlyChild = transform.GetChild(0).GetComponent<IBehaviourTree>();
    }

    public bool IsDisabled()
    {
        return disabled;
    }

    public BehaviourTreeResult Tick(BehaviourTreeBossBlackboard _blackboard)
    {
        name = " tick ";
        BehaviourTreeResult result = onlyChild.Tick(_blackboard);
        return result == BehaviourTreeResult.Sucess? BehaviourTreeResult.Fail : BehaviourTreeResult.Sucess;
    }
}
