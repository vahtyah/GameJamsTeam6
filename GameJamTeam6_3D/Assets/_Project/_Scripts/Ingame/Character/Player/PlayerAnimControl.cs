using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerAnimControl : MonoBehaviour
{
    readonly static Dictionary<PlayerAnimState, string> animNames = new Dictionary<PlayerAnimState, string>()
    {
        //{PlayerAnimState.Idle, "Idle"},
        //{PlayerAnimState.Walk, "Walk" },
        //{PlayerAnimState.NormalAttack, "NormalAttack" },
        {PlayerAnimState.Move, "Run" },
        {PlayerAnimState.Die, "Die" }
    };

    const string moveBlendAnimX = "MoveX";
    const string moveBlendAnimY = "MoveY";

    [SerializeField] Animator animator;
    string currentAnim;

    public void PlayAnim(PlayerAnimState _state)
    {
        if (currentAnim == animNames[_state]) return;
        animator.Play(animNames[_state]);
        currentAnim = animNames[_state];
    }

    public void SetMovementBlend(float _blendX, float _blendY)
    {
        animator.SetFloat(moveBlendAnimX, _blendX);
        animator.SetFloat(moveBlendAnimY, _blendY);
    }

    public void SetAnimSpeed(float _speed)
    {
        
    }

}

public enum PlayerAnimState
{
    //Idle,
    Move,
    Die,
    //Walk,
    //NormalAttack,
}

