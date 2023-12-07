using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class EnemyBehaviourTree : SerializedMonoBehaviour
{
    [SerializeField] IBehaviourTree baseBranch;
    [SerializeField] BehaviourTreeBossBlackboard blackboard;
    public BehaviourTreeBossBlackboard Blackboard => blackboard;

    public void OnUpdate()
    {
        blackboard.SetLastResult(baseBranch.Tick(blackboard));
    }

}

public interface IBTBlackboard
{

    public BehaviourTreeResult lastResult { get => lastResult; set => lastResult = value; }

    public void SetInterfaceLastResult(BehaviourTreeResult _lastResult)
    {
        lastResult = _lastResult;
    }
    public Dictionary<BehaviourTreeBlackboardInfo, bool> agentInfo { get; set; }
    public void AssignBlackBoard(BehaviourTreeBlackboardInfo _info, bool _result)
    {
        agentInfo[_info] = _result;
    }
    public bool GetInfo(BehaviourTreeBlackboardInfo _info)
    {
        if (agentInfo.ContainsKey(_info) == false)
            return false;
        return agentInfo[_info];
    }

    public IBTBlackboard GetBlackboard();

}