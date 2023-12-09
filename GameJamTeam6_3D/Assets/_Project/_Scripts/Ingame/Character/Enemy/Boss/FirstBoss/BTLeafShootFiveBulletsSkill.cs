using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTLeafShootFiveBulletsSkill : MonoBehaviour, IEnemySkill, IBehaviourTree
{
    const int totalBulletsOut = 5;
    [SerializeField] float coolDown = 5;
    [SerializeField] int angleSpread = 30;
    [SerializeField] Transform shootPos;
    float[] selectedAngles;
    float nextTime = 0;
    
    [SerializeField] int damage = 10;


    void Awake()
    {
        nextTime = coolDown;
        selectedAngles = new float[totalBulletsOut];
        for (int i = 0; i < selectedAngles.Length; i++)
        {
            selectedAngles[i] = -angleSpread + i * 2 * (angleSpread / (totalBulletsOut - 1));
        }
        nextTime = coolDown + Time.time;
    }

    public bool IsReady()
    {
        return Time.time >= nextTime;
        //nextTime -= Time.deltaTime;
        //return nextTime <= 0;
    }

    public void UseSkill()
    {
        for (int i = 0; i < selectedAngles.Length; i++)
        {
            IProjectile bullet = ProjectilePooling.instance.ActivateProjectile(0).SetDamage(DamageHelper.GetPlayerDamage(damage));
            bullet.SetPossession(_isPlayer: true);
            bullet.GetGameObject().transform.position = shootPos.position;
            bullet.GetGameObject().transform.rotation = boss.GetGameObject().transform.rotation;
            bullet.GetGameObject().transform.Rotate(bullet.GetGameObject().transform.rotation.x
            , bullet.GetGameObject().transform.rotation.y + selectedAngles[i]
            , bullet.GetGameObject().transform.rotation.z
            );
        }
        nextTime = Time.time + coolDown;
    }

    IBoss boss;
    public BehaviourTreeResult Tick(BehaviourTreeBossBlackboard _blackboard)
    {
        boss = _blackboard.GetAgent();
        if (IsReady())
        {
            UseSkill();
            return BehaviourTreeResult.Sucess;
        }
        return BehaviourTreeResult.Fail;
    }

    [SerializeField] bool disabled;
    public bool IsDisabled()
    {
        return disabled;
    }
}