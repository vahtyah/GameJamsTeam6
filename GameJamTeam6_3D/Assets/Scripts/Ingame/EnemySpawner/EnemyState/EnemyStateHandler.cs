using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateHandler : SerializedMonoBehaviour
{
    [SerializeField] Dictionary<EnemyState, IEnemyState> enemyStates;
    [SerializeField] IEnemy enemy;
    IEnemyState currentState;
    [Header("For Debug")]
    [SerializeField] EnemyState debug;

    private void Awake()
    {
        currentState = enemyStates[EnemyState.Idle];
        foreach (var keyValue in enemyStates)
        {
            keyValue.Value.SetEnemy(enemy);
        }
    }
    private void Update()
    {
        debug = currentState.enemyState;
    }
    public void SetState(EnemyState _state)
    {
        currentState = enemyStates[_state];
    }

    public void OnUpdate()
    {
        EnemyState state = currentState.OnUpdate();
        if (currentState.enemyState != state)
        {
            currentState.OnExit();
            currentState = enemyStates[state];
            currentState.OnEnter();
        }
    }
}


public interface IEnemyState
{
    EnemyState enemyState { get; }
    public void SetEnemy(IEnemy _enemy);
    public void OnEnter();
    public void OnExit();
    public EnemyState OnUpdate();
}
