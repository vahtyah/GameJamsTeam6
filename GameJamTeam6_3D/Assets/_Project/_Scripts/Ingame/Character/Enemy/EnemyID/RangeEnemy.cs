using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent (typeof(Rigidbody))]
[RequireComponent (typeof(BoxCollider))]
public class RangeEnemy : SerializedMonoBehaviour, IRangeEnemy
{
    [SerializeField] NavMeshAgent agent;
    [SerializeField] Animator animator;
    [SerializeField] EnemyStateHandler enemyStateHandler;
    [SerializeField] EnemyData enemyData;
    EnemyData curEnemyData;
    EnemyNav enemyNav = new EnemyNav();
    [SerializeField] CharacterHealth characterHealth = new CharacterHealth();
    EnemyAnimController anim = new EnemyAnimController();
    int atWave = -1;

    void Awake()
    {
        curEnemyData = enemyData;
        tag = GlobalInfo.enemyTagAndLayer;
        gameObject.layer = LayerMask.NameToLayer(tag);
        agent = GetComponent<NavMeshAgent>();
        characterHealth.Setup(enemyData.hp);
        characterHealth.AddSignalHealthChange((curHealth) =>
        {
        });
        characterHealth.AddSignalOnDead(() =>
        {
            //gameObject.SetActive(false);
            OnDie();
        });
        enemyNav.SetAgent(agent);
    }
    void OnEnable()
    {
        characterHealth.Setup(curEnemyData.hp);
        agent.enabled = true;
    }

    void Update()
    {
        enemyStateHandler.OnUpdate();
    }

    public GameObject GetGameObject()
    {
        return gameObject;
    }

    public void AddHealth(int _inputDamage)
    {
        characterHealth.AddHealth(_inputDamage);
    }

    public IEnemy SetThisEnemyFromWave(int _wave)
    {
        atWave = _wave;
        return this;
    }

    public IEnemy Setup()
    {
        characterHealth.Setup(100);
        anim.SetAnimator(animator);
        enemyNav.SetAnimController(anim).SetSpeed(enemyData.speed);
        return this;
    }

    public EnemyNav GetEnemyNav()
    {
        return enemyNav;
    }
    int IEnemy.GetWave() { return atWave;}

    public EnemyData GetEnemyData()
    {
        return curEnemyData;
    }

    public CharacterHealth GetCharacterHealth() => characterHealth;

    [SerializeField] Transform shootPlace;
    public Transform GetShootTransform()
    {
        return shootPlace;
    }

    public void OnDie()
    {
        EnemySpawner.instance.OnEnemyDie(atWave);
        enemyStateHandler.ForceState(EnemyAnimState.Die);
    }

    public EnemyAnimController GetAnim()
    {
        return anim;
    }
}