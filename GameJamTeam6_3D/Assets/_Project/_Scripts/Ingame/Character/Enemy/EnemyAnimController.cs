using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class EnemyAnimController 
{
    readonly static Dictionary<EnemyAnimState, string> animNames = new Dictionary<EnemyAnimState, string>()
    {
        {EnemyAnimState.Idle, "Idle"}, {EnemyAnimState.Attack, "Attack"},
        {EnemyAnimState.GetHurt, "GetHurt" },
        {EnemyAnimState.Move, "Move"},
        {EnemyAnimState.Die, "Die" }
    };
    
    Animator animator;
    string currentAnim;
    
    public EnemyAnimController SetAnimator(Animator _animator)
    {
        animator = _animator;
        return this;
    }

    public void PlayAnim(EnemyAnimState _state)
    {
        if (currentAnim == animNames[_state]) return;
        animator.Play(animNames[_state]);
        currentAnim = animNames[_state];
    }
}

public enum EnemyAnimState
{
    Idle, Move, Attack, Die, GetHurt
}
