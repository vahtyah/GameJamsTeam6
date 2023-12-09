using UnityEngine;

public class PlayerAnimationTrigger : MonoBehaviour
{
    public void FinishDiveRoll()
    {
        Player.instance.GetStateHandler().SetState(PlayerAnimState.NormalMovement);
    }
}