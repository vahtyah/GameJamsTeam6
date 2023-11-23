using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent (typeof(Rigidbody))]
[RequireComponent (typeof(BoxCollider))]
public class ExampleEnemy : MonoBehaviour, IEnemy
{
    [SerializeField] NavMeshAgent agent;
    [SerializeField] Animator animator;
    [SerializeField] EnemyData enemyData;
    EnemyData curEnemyData;
    [SerializeField] EnemyStateHandler enemyStateHandler;
    EnemyNav enemyNav = new EnemyNav();
    CharacterHealth characterHealth = new CharacterHealth();
    EnemyAnimController anim = new EnemyAnimController();
    int atWave = -1;

    void Awake()
    {
        curEnemyData = enemyData;
        tag = GlobalString.enemyTagAndLayer;
        gameObject.layer = LayerMask.NameToLayer(tag);
        agent = GetComponent<NavMeshAgent>();
        characterHealth.AddSignalHealthChange((curHealth) =>
        {

        });
        characterHealth.AddSignalOnDead(() =>
        {
            gameObject.SetActive(false);
            EnemySpawner.instance.OnEnemyDie(atWave);
        });
    }

    void OnEnable()
    {
        characterHealth.Setup(curEnemyData.hp, curEnemyData.hp);
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
        enemyNav.SetAnimController(anim).SetSpeed(8).SetAgent(agent);
        return this;
    }

    public EnemyNav GetEnemyNav()
    {
        return enemyNav;
    }

    public int GetDamage() { throw new System.NotImplementedException(); }
    public int GetWave() { return atWave;}

    public EnemyData GetEnemyData()
    {
        return curEnemyData;
    }
}


