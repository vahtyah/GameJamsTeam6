
using UnityEngine;

public interface IEnemy : IEnemyHealth
{
    public IEnemy Setup();

    public GameObject GetGameObject();
    public IEnemy SetThisEnemyFromWave(int _wave);

    

    public EnemyNav GetEnemyNav();
    public int GetWave();
    public EnemyData GetEnemyData();
    
    public CharacterHealth GetCharacterHealth();

    public void OnDie();

    public EnemyAnimController GetAnim();
}

public interface IRangeEnemy : IEnemy
{
    public Transform GetShootTransform();
}

public interface IEnemyHealth
{
    public void AddHealth(int _input);
}

public interface IBoss : IEnemyHealth
{
    public void Setup();
    public GameObject GetGameObject();
    public EnemyNav GetEnemyNav();
    public EnemyData GetEnemyData();
    public CharacterHealth GetCharacterHealth();
    public EnemyAnimController GetEnemyAnimController();
    public EnemyBehaviourTree GetBehaviourTree();

    public void OnDie();

    float considerAtLowHealthPercent { get; set; }
    // ???? to clean code 
    public void OnCurrentHealthChange(int _health)
    {
        GetBehaviourTree().Blackboard.AssignBlackBoard(BehaviourTreeBlackboardInfo.SelfEnemyLowHealth
            , ()=> (float)_health / (float)GetCharacterHealth().MaxHealth <= considerAtLowHealthPercent);
    }
}



