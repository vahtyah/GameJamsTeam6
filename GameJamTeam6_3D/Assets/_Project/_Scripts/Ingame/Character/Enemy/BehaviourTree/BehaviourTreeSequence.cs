using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourTreeSequence : SerializedMonoBehaviour, IBehaviourTree
{
    [SerializeField] bool disabled = false;
    [SerializeField] IBehaviourTree lastChild;
    BehaviourTreeBossBlackboard blackboard;
    [SerializeField] List<IBehaviourTree> childs = new List<IBehaviourTree>();

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
        name = "Tick";
        if (_blackboard.LastResult == BehaviourTreeResult.Running)
        {
            return lastChild.Tick(_blackboard);
        }
        for (int i = 0; i < childs.Count; i++)
        {
            BehaviourTreeResult result = childs[i].Tick(_blackboard);
            ColorDebug.DebugRed("run");
            lastChild = childs[i];
            switch (result)
            {
                case BehaviourTreeResult.Sucess:
                    continue;
                case BehaviourTreeResult.Running:
                    return result;
                case BehaviourTreeResult.Fail:
                    return BehaviourTreeResult.Fail;
            }
        }
        return BehaviourTreeResult.Sucess;
    }
}
