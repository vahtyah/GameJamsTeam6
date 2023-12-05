using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class EnemyBehaviourTree : SerializedMonoBehaviour
{
    [SerializeField] IBehaviourTree baseBranch;
    //[SerializeField] BehaviourTreeIterateType iterateType = BehaviourTreeIterateType.Update;
    [SerializeField] BehaviourTreeBlackboard blackboard;
    public BehaviourTreeBlackboard Blackboard;

    //bool ok = false;

    //public void Active(bool _ok = true)
    //{
    //    ok = _ok;
    //}

    public void OnUpdate()
    {
        blackboard.SetLastResult(baseBranch.Tick(blackboard));
    }

    //public void Update()
    //{
    //    if (ok == false && iterateType != BehaviourTreeIterateType.Update) return;
    //    blackboard.SetLastResult(baseBranch.Tick(blackboard));
    //}

    //private void FixedUpdate()
    //{
    //    if (ok == false && iterateType != BehaviourTreeIterateType.FixedUpdate) return;
    //    blackboard.SetLastResult(baseBranch.Tick(blackboard));
    //}

}

//public enum BehaviourTreeIterateType
//{
//    Update, FixedUpdate
//}