using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FirstBoss : MonoBehaviour, IBoss
{
    [SerializeField] NavMeshAgent agent;
    [SerializeField] Animator animator;
    [SerializeField] EnemyBehaviourTree behaviourTree;
    [SerializeField] EnemyData enemyData;
    CharacterHealth characterHealth = new CharacterHealth();
    EnemyAnimController anim = new EnemyAnimController();
    EnemyNav enemyNav = new EnemyNav();

    // Start is called before the first frame update

    void Awake()
    {
        characterHealth.onCurHealthChange = OnCurrentHealthChange;
        characterHealth.onDead = OnDie;
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        behaviourTree.OnUpdate();
    }

    public void AddHealth(int _input)
    {
        characterHealth.AddHealth(_input);
    }

    public CharacterHealth GetCharacterHealth()
    {
        return characterHealth;
    }

    public EnemyData GetEnemyData()
    {
        return enemyData;
    }

    public EnemyNav GetEnemyNav()
    {
        return enemyNav;
    }

    public GameObject GetGameObject()
    {
        return gameObject;
    }

    [Range(0, 1f)]
    [SerializeField] float considerAtLowHealthPercent = 0.2f;
    private void OnCurrentHealthChange(int _health)
    {
        if ((float)_health / (float)characterHealth.MaxHealth <= considerAtLowHealthPercent)
            behaviourTree.Blackboard.AssignBlackBoard(BehaviourTreeBlackboardInfo.SelfEnemyLowHealth, true);
        else
        {
            if (behaviourTree.Blackboard.GetInfo(BehaviourTreeBlackboardInfo.SelfEnemyLowHealth) == false) return;
            behaviourTree.Blackboard.AssignBlackBoard(BehaviourTreeBlackboardInfo.SelfEnemyLowHealth, false);
        }
    }


    public void OnDie()
    {
        
    }

    public void Setup()
    {
        enemyNav.SetAgent(agent);
        enemyNav.SetAnimController(anim);
        enemyNav.SetSpeed(enemyData.speed);

    }

    public EnemyAnimController GetEnemyAnimController()
    {
        return anim;
    }
}
