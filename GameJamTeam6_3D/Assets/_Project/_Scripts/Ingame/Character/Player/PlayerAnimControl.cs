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
        {PlayerAnimState.NormalMovement, "Move" },
        {PlayerAnimState.Die, "Die" },
        {PlayerAnimState.EquipmentInventory, "EquipmentInventory" },
        {PlayerAnimState.DiveRoll, "DiveRoll" },
    };

    const string moveBlendAnimX = "MoveX";
    const string moveBlendAnimY = "MoveY";

    [SerializeField] Animator animator;
    string currentAnim;

    public void PlayAnim(PlayerAnimState _state)
    {
        ColorDebug.DebugRed(_state.ToString());
        if (currentAnim == animNames[_state]) return;
        animator.Play(animNames[_state]);
        currentAnim = animNames[_state];
    }

    public void SetMovementBlend(float _blendX, float _blendY)
    {
        animator.SetFloat(moveBlendAnimX, _blendX);
        animator.SetFloat(moveBlendAnimY, _blendY);
    }

    public float GetCurrentAnimLength()
    {
        return animator.GetCurrentAnimatorStateInfo(0).length;
    }

    //public void SetAnimSpeed(float _speed)
    //{
        
    //}

}

public enum PlayerAnimState
{
    //Idle,
    NormalMovement,
    Die,
    EquipmentInventory,
    DiveRoll,
    //Walk,
    //NormalAttack,
}

