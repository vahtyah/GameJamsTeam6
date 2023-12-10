using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTLeafCustomDropDamageAreaSkill : MonoBehaviour, IEnemySkill, IBehaviourTree
{
    [SerializeField] bool disabled;
    [SerializeField] EnemyDamageCollisionImminent[] enemyDamageCollisionImminents;
    [SerializeField] Transform collisionsHolder;
    [SerializeField] float coolDown = 5;
    RunningActionProgressBehaving progressBehave = new RunningActionProgressBehaving();
    float timer;

    bool ok = false;

    void Awake()
    {
        timer = Time.time + coolDown;
        enemyDamageCollisionImminents[0].complete = ()=> ok = true;
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
            return progressBehave.Progress(
                _onEnter: () =>
                {
                    _blackboard.GetAgent().GetGameObject().transform.LookAt(Player.instance.transform.position);
                    //position = _blackboard.GetAgent().GetGameObject().transform.position;
                    UseSkill();
                }
                , _onRunning: null
                , ref ok
                , ()=> timer = Time.time + coolDown
                ); ;
        }
        return BehaviourTreeResult.Fail;
    }

    public void UseSkill()
    {
        collisionsHolder.transform.position = transform.position;
        for (int i = 0; i < enemyDamageCollisionImminents.Length; i++)
        {
            enemyDamageCollisionImminents[i].StartComing();
        }
        //timer = Time.time + coolDown;
    }
}
