using UnityEngine;

public class PlayerNormalMovementState : PlayerState
{
    public override PlayerAnimState playerState => PlayerAnimState.NormalMovement;

    public override PlayerAnimState OnUpdate()
    {
        player.GetMovement().Iterate();
        if (player.GetWeapon().CanAttack())
            player.GetWeapon().Shoot();
        return !player.IsLive() ? PlayerAnimState.Die : base.OnUpdate();
    }
    
    public override void OnExit()
    {
        base.OnExit();
        player.GetRb().velocity = Vector3.zero;
    }
}