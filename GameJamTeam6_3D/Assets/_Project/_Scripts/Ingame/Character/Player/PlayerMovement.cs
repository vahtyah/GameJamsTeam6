using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Player;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //[SerializeField] Transform model;
    int speed = 10;
    float cameraDistance = 10f;

    public void Setup(int _speed)
    {
        speed = _speed;
        cameraDistance = Camera.main.transform.position.magnitude;
    }

    public void Iterate()
    {
        Vector2 normalizedMoveInput = new Vector3(InputHandler.instance.HorizontalMovement(), InputHandler.instance.VerticalMovement());
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, cameraDistance));
        Move(normalizedMoveInput);
        Look(mousePos);
        BlenAnim(mousePos, normalizedMoveInput);
        //Vector3 lookDir = mousePos - Player.instance.transform.position;
        //lookDir = new Vector3(lookDir.x, 0, lookDir.z);
        //Vector2 lookDirNormal = new Vector2(lookDir.x, lookDir.z).normalized;

        //float angle = Vector2.SignedAngle(normalizedMoveInput, lookDirNormal);
        //Vector2 blendValue = new Vector2( Quaternion.AngleAxis(angle, Vector2.up).eulerAngles.x, Quaternion.AngleAxis(angle, Vector2.up).eulerAngles.z);

        //Player.instance.GetAnimControl().SetMovementBlend(blendValue.x, blendValue.y);
        
    }

    void Move(Vector2 _normalized)
    {
        float moveSpeed = speed * Time.fixedDeltaTime;
        Player.instance.GetRb().velocity =
            new Vector3(_normalized.x * moveSpeed, 0, _normalized.y * moveSpeed);
    }

    void Look(Vector3 _mousePos)
    {
        _mousePos = new Vector3(_mousePos.x, 0, _mousePos.z);   
        Player.instance.GetModel().LookAt(_mousePos);
        Player.instance.GetModel().eulerAngles = new Vector3(0, Player.instance.GetModel().eulerAngles.y, 0);
    }
    const float addedThresholdAngle = 90;
    void BlenAnim(Vector3 mousePos, Vector2 normalizedMoveInput)
    {
        Vector3 lookDir = mousePos - Player.instance.transform.position;
        lookDir = new Vector3(lookDir.x, 0, lookDir.z);
        Vector2 lookDirNormal = new Vector2(lookDir.x, lookDir.z).normalized;
        float angle =( Vector2.Angle(lookDirNormal, normalizedMoveInput) + addedThresholdAngle) * Mathf.Deg2Rad;
        ColorDebug.DebugRed(angle);
        Vector2 value = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
        ColorDebug.DebugOrange(value);
        if ((angle - Mathf.Deg2Rad * addedThresholdAngle) == 0) value = Vector2.zero;

        Player.instance.GetAnimControl().SetMovementBlend(value.x, value.y);
        
    }

}
