
using UnityEngine;

public interface IEnemy 
{
    public IEnemy Setup();

    public GameObject GetGameObject();
    public IEnemy SetThisEnemyFromWave(int _wave);

    public void OnBeaten(int _inputDamage);

    public EnemyNav GetEnemyNav();
    public int GetDamage();
    public int GetWave();
    EnemyStateHandler GetEnemyStateHandler();
}

public enum EnemyAnimState
{
    Idle, Move, Attack, Die
}