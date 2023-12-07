using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTLeafCustomDropDamageAreaSkill : MonoBehaviour, IEnemySkill, IBehaviourTree
{
    [SerializeField] bool disabled;
    [SerializeField] EnemyDamageCollisionImminent[] enemyDamageCollisionImminents;
    [SerializeField] Transform collisionsHolder;
    [SerializeField] float coolDown = 5;
    float timer;

    void Awake()
    {
        timer = Time.time + coolDown;
    }

    public bool IsDisabled()
    {
        return disabled;
    }

    public bool IsReady()
    {
        return Time.time >= timer;
    }
    Vector3 position;
    public BehaviourTreeResult Tick(BehaviourTreeBossBlackboard _blackboard)
    {
        if (IsReady())
        {
            position = _blackboard.GetAgent().GetGameObject().transform.position;
            UseSkill();
            return BehaviourTreeResult.Sucess;
        }
        return BehaviourTreeResult.Fail;
    }

    public void UseSkill()
    {
        collisionsHolder.transform.position = position;
        for (int i = 0; i < enemyDamageCollisionImminents.Length; i++)
        {
            enemyDamageCollisionImminents[i].StartComing();
        }
        timer = Time.time + coolDown;
    }
}
