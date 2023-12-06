using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourTreeSequence : MonoBehaviour, IBehaviourTree
{
    [SerializeField] bool disabled = false;
    IBehaviourTree lastChild;
    BehaviourTreeBossBlackboard blackboard;
    List<IBehaviourTree> childs = new List<IBehaviourTree>();

    void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            childs.Add(transform.GetChild(i).GetComponent<IBehaviourTree>());
        }
    }

    public bool IsDisabled()
    {
        return disabled;
    }

    public void SetBlackboard(BehaviourTreeBossBlackboard _blackboard)
    {
        blackboard = _blackboard;
    }

    public BehaviourTreeResult Tick(BehaviourTreeBossBlackboard _blackboard)
    {
        if (_blackboard.LastResult == BehaviourTreeResult.Running)
        {
            return lastChild.Tick(_blackboard);
        }
        for (int i = 0; i < childs.Count; i++)
        {
            BehaviourTreeResult result = childs[i].Tick(_blackboard);
            lastChild = childs[i];
            switch (result)
            {
                case BehaviourTreeResult.Sucess:
                    continue;
                case BehaviourTreeResult.Running:
                    return result;
                case BehaviourTreeResult.Fail:
                    break;
            }
            break;
        }
        return BehaviourTreeResult.Sucess;
    }
}
