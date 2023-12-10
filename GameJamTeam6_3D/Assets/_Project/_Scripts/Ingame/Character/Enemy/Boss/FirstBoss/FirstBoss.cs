using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FirstBoss : SerializedMonoBehaviour, IBoss
{
    [SerializeField] NavMeshAgent agent;
    [SerializeField] Animator animator;
    [SerializeField] EnemyBehaviourTree behaviourTree;
    [SerializeField] EnemyData enemyData;
    [SerializeField] IEnemySkill[] enemySkills;
    CharacterHealth characterHealth = new CharacterHealth();
    EnemyAnimController anim = new EnemyAnimController();
    EnemyNav enemyNav = new EnemyNav();


    // Start is called before the first frame update

    void Awake()
    {
        characterHealth.onCurHealthChange = (this as IBoss).OnCurrentHealthChange;
        characterHealth.onDead += OnDie;
    }
    void Start()
    {
        for (int i = 0; i < enemySkills.Length; i++)
        {
            if (GlobalInfo.enemyAbilityInfo.ContainsKey(i) == false) break;
            behaviourTree.Blackboard.AssignBlackBoard(GlobalInfo.enemyAbilityInfo[i], () => enemySkills[i].IsReady());
        }
        MapSceneManager.instance.GetCurrentMap().ActivePortals(false);
        anim.SetAnimator(animator);
        enemyNav.SetAgent(agent);
        enemyNav.SetAnimController(anim);
        enemyNav.SetSpeed(5);
    }
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

    float IBoss.considerAtLowHealthPercent { get => considerAtLowHealthPercent; set => considerAtLowHealthPercent = value; }

    public void OnDie()
    {
        gameObject.SetActive(false);
        MapSceneManager.instance.GetCurrentMap().ActivePortals(true);
        IngameManager.instance.Win();
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

    public EnemyBehaviourTree GetBehaviourTree()
    {
        return behaviourTree;
    }
}
