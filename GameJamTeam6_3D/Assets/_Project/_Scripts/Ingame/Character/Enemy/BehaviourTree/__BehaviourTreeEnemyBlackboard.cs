using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourTreeEnemyBlackboard : MonoBehaviour, IBTBlackboard
{
    public Dictionary<BehaviourTreeBlackboardInfo, bool> agentInfo { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    public IBTBlackboard GetBlackboard()
    {
        return this;
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
