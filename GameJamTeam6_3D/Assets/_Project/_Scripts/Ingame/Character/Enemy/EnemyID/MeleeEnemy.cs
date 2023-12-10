using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent (typeof(Rigidbody))]
[RequireComponent (typeof(BoxCollider))]
public class MeleeEnemy : MonoBehaviour, IEnemy
{
    [SerializeField] NavMeshAgent agent;
    [SerializeField] Animator animator;
    [SerializeField] EnemyData enemyData;
    EnemyData curEnemyData;
    [SerializeField] EnemyStateHandler enemyStateHandler;
    [SerializeField] GameObject enemyMeleeDamageCollision;
    EnemyNav enemyNav = new EnemyNav();
    CharacterHealth characterHealth = new CharacterHealth();
    EnemyAnimController anim = new EnemyAnimController();
    public EnemyAnimController GetAnim() => anim;
    int atWave = -1;

    void Awake()
    {
        curEnemyData = enemyData;
        tag = GlobalInfo.enemyTagAndLayer;
        gameObject.layer = LayerMask.NameToLayer(tag);
        agent = GetComponent<NavMeshAgent>();
        characterHealth.AddSignalHealthChange((curHealth) =>
        {

        });
        characterHealth.AddSignalOnDead(() => OnDie());
        enemyNav.SetAgent(agent);
    }

    void OnEnable()
    {
        characterHealth.Setup(curEnemyData.hp);
        //agent.enabled = true;
    }

    void Update()
    {
        enemyStateHandler.OnUpdate();
    }

    public GameObject GetGameObject()
    {
        return gameObject;
    }

    public void AddHealth(int _input)
    {
        characterHealth.AddHealth(_input);
    }

    public IEnemy SetThisEnemyFromWave(int _wave)
    {
        atWave = _wave;
        return this;
    }

    public IEnemy Setup()
    {
        characterHealth.Setup(enemyData.hp);
        anim.SetAnimator(animator);
        enemyNav.SetAnimController(anim).SetSpeed(enemyData.speed);
        //agent.enabled = false;
        return this;
    }

    public EnemyNav GetEnemyNav()
    {
        return enemyNav;
    }

    public int GetDamage() { return 10; }
    public int GetWave() { return atWave;}

    public EnemyData GetEnemyData()
    {
        return curEnemyData;
    }

    public CharacterHealth GetCharacterHealth() => characterHealth;

    public void OnDie()
    {
        enemyMeleeDamageCollision.SetActive(false);
        EnemySpawner.instance.OnEnemyDie(atWave);
        //enabled = false;
        enemyStateHandler.ForceState(EnemyAnimState.Die);
    }
}


