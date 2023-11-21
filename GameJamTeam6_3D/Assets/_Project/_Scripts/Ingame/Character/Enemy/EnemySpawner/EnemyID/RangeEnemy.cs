using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent (typeof(Rigidbody))]
[RequireComponent (typeof(BoxCollider))]
public class RangeEnemy : MonoBehaviour, IEnemy
{
    [SerializeField] NavMeshAgent agent;
    [SerializeField] Animator animator;
    [SerializeField] EnemyStateHandler enemyStateHandler;
    [SerializeField] private int damage;
    EnemyNav enemyNav = new EnemyNav();
    CharacterHealth characterHealth = new CharacterHealth();
    EnemyAnimController anim = new EnemyAnimController();
    int atWave = -1;

    void Awake()
    {
        tag = GlobalString.enemyTagAndLayer;
        gameObject.layer = LayerMask.NameToLayer(tag);
        agent = GetComponent<NavMeshAgent>();
        characterHealth.AddSignalHealthChange((curHealth) =>
        {
        });
        characterHealth.AddSignalOnDead(() =>
        {
            EnemySpawner.instance.OnEnemyDie(atWave);
        });
    }

    void Update()
    {
        enemyStateHandler.OnUpdate();
    }

    public GameObject GetGameObject()
    {
        return gameObject;
    }

    public void OnBeaten(int _inputDamage)
    {
        characterHealth.AddHealth(-_inputDamage);
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
        enemyNav.SetAnimController(anim).SetSpeed(12).SetAgent(agent).Move();
        return this;
    }

    public EnemyNav GetEnemyNav()
    {
        return enemyNav;
    }

    int IEnemy.GetDamage() { return damage;}
    int IEnemy.GetWave() { return atWave;}
    public EnemyStateHandler GetEnemyStateHandler() { return enemyStateHandler; }
}