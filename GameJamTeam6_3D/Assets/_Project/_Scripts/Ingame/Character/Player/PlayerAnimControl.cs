using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerAnimControl : MonoBehaviour
{
    readonly static Dictionary<PlayerAnimState, string> animNames = new Dictionary<PlayerAnimState, string>()
    {
        {PlayerAnimState.Idle, "Idle"},
        {PlayerAnimState.Walk, "Walk" },
        {PlayerAnimState.NormalAttack, "NormalAttack" },
        {PlayerAnimState.Run, "Run" },
    };
    [SerializeField] Animator animator;
    string currentAnim;

    public void PlayAnim(PlayerAnimState _state)
    {
        if (currentAnim == animNames[_state]) return;
        animator.Play(animNames[_state]);
        currentAnim = animNames[_state];
    }







}

public enum PlayerAnimState
{
    Idle,
    Run,
    Walk,
    NormalAttack,
}

