
using UnityEngine;

public interface IEnemy 
{
    public IEnemy Setup();

    public GameObject GetGameObject();
    public IEnemy SetThisEnemyFromWave(int _wave);

    public void AddHealth(int _input);

    public EnemyNav GetEnemyNav();
    public int GetWave();
    public EnemyData GetEnemyData();
    
    public CharacterHealth GetCharacterHealth();

    public void OnDie();

}

public interface IRangeEnemy : IEnemy
{
    public Transform GetShootTransform();
}

public interface IBoss
{
    public void Setup();
    public GameObject GetGameObject();
    public EnemyNav GetEnemyNav();
    public EnemyData GetEnemyData();
    public CharacterHealth GetCharacterHealth();
    public EnemyAnimController GetEnemyAnimController();
    public void OnDie();
}



