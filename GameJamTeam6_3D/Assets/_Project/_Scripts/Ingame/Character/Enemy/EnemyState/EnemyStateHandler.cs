using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateHandler : SerializedMonoBehaviour
{
    [SerializeField] Dictionary<EnemyAnimState, IEnemyState> enemyStates;
    [SerializeField] IEnemy enemy;
    [Space]
    [SerializeField] IEnemyState currentState;
    //[Header("For Debug")]
    //[SerializeField] EnemyAnimState debug;
    [SerializeField] bool active = true;

    private void Awake()
    {
        currentState = enemyStates[EnemyAnimState.Idle];
        foreach (var keyValue in enemyStates)
        {
            keyValue.Value.SetEnemy(enemy);
        }
    }

    public void ForceState(EnemyAnimState _state)
    {
        currentState.OnExit();
        currentState = enemyStates[_state];
        currentState.OnEnter();
    }

    public void OnUpdate()
    {
        EnemyAnimState state = currentState.OnUpdate();
        if (currentState.enemyState != state)
        {
            currentState.OnExit();
            currentState = enemyStates[state];
            currentState.OnEnter();
        }
    }

    public IEnemyState GetState(EnemyAnimState state) { return enemyStates[state]; }
}


public interface IEnemyState
{
    EnemyAnimState enemyState { get; }
    public void SetEnemy(IEnemy _enemy);
    public void OnEnter();
    public void OnExit();
    public EnemyAnimState OnUpdate();
}
