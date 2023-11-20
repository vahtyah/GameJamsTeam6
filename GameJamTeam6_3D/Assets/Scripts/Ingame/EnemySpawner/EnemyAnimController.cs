using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class EnemyAnimController 
{
    const string move = "Move";
    const string idle = "Idle";
    const string attack = "Attack";

    readonly Dictionary<EnemyState, string> animNames = new Dictionary<EnemyState, string>()
    {
        {EnemyState.Idle, idle}, {EnemyState.Attack, attack}, {EnemyState.Move, move}
    };

    Animator animator;
    string currentAnim;
     
    public EnemyAnimController SetAnimator(Animator _animator)
    {
        animator = _animator;
        return this;
    }

    public void PlayAnim(EnemyState _state)
    {
        if (currentAnim == animNames[_state]) return;
        animator.Play(animNames[_state]);
        currentAnim = animNames[_state];
    }
}


