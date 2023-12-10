using UnityEngine;
using System.Collections;

public class PlayerDiveRollState : PlayerState
{
    public override PlayerAnimState playerState => PlayerAnimState.DiveRoll;
    private Vector3 direction;

    public override void OnEnter()
    {
        base.OnEnter();
        float cameraDistance = Vector3.Distance(Camera.main.transform.position, player.transform.position);
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, cameraDistance));
        direction = (mousePos - player.transform.position).normalized;
        direction.y = 0;
    }

    public override PlayerAnimState OnUpdate()
    {
        player.GetRb().MovePosition(direction * (10 * Time.deltaTime) + player.transform.position);
        if(!player.IsLive()) return PlayerAnimState.Die;
        return base.OnUpdate();
    }
}