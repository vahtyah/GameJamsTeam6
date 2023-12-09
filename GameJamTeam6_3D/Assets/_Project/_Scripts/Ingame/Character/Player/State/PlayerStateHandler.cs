using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class PlayerStateHandler : SerializedMonoBehaviour
{
    [SerializeField] private Dictionary<PlayerAnimState, PlayerState> playerStates;
    [ShowInInspector]
    PlayerState currentState;

    public PlayerState CurrentState
    {
        get => currentState;
        set
        {
            currentState?.OnExit();
            currentState = value;
            currentState?.OnEnter();
        }
    }
    
    private void Start()
    {
        CurrentState = playerStates[PlayerAnimState.NormalMovement];
    }
    
    public void SetState(PlayerAnimState _state)
    {
        CurrentState = playerStates[_state];
    }
    
    public void Update()
    {
        PlayerAnimState state = currentState.OnUpdate();
        if (currentState.playerState == state) return;
        CurrentState = playerStates[state];
    }
}