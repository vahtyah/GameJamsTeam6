using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourTreeBossBlackboard : SerializedMonoBehaviour
{
    [SerializeField] IBoss agent;
    public IBoss GetAgent() { 

        return agent;
    }
    [SerializeField] Dictionary<BehaviourTreeBlackboardInfo, bool> agentInfo;

    BehaviourTreeResult lastResult;
    public BehaviourTreeResult LastResult => lastResult;


    public void SetLastResult(BehaviourTreeResult _lastResult)
    {
        lastResult = _lastResult;
    }

    public void AssignBlackBoard(BehaviourTreeBlackboardInfo _info, bool _result)
    {
        if (agentInfo[_info] == _result) return;
        agentInfo[_info] = _result;
    }

    public bool GetInfo(BehaviourTreeBlackboardInfo _info)
    {
        if (agentInfo.ContainsKey(_info) == false)
            return false;
        return agentInfo[_info];
    }

}

public enum BehaviourTreeBlackboardInfo
{
    PlayerLowHeath,
    PlayerFullHealth,
    SelfEnemyLowHealth,

    EnemyAbilityOneReady,
    EnemyAbilityTwoReady,
    EnemyAbilityThreeReady,
    EnemyAbilityFourReady,
    EnemyAbilityFiveReady,
    EnemyAbilitySixReady,

    NormalAttackReady,
    
    NearPlayer,

}
