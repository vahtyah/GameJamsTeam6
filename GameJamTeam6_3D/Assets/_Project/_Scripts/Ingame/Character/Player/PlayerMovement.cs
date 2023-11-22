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
        Vector3 mousePos = Camera.main.ScreenToViewportPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, cameraDistance));
        Vector3 lookDir = mousePos - Player.instance.transform.position;
        Vector2 lookDirNormal = new Vector2(lookDir.x, lookDir.z).normalized;
        Move(normalizedMoveInput);
        Look(lookDir);
        //float angleDeg = Vector2.Angle(normalizedMoveInput, lookDirNormal);
        Player.instance.GetAnimControl().SetMovementBlend(lookDirNormal.x, lookDirNormal.y);
        //if (angleDeg > 160f)
        //{
        //    Player.instance.GetAnimControl().SetMovementBlend(lookDirNormal.x, lookDirNormal.y);
        //}
        //else
        //{

        //}
    }

    void Move(Vector2 _normalized)
    {
        float moveSpeed = speed * Time.fixedDeltaTime;
        Player.instance.GetRb().velocity =
            new Vector3(_normalized.x * moveSpeed, 0, _normalized.y * moveSpeed);
    }

    void Look(Vector3 _lookDir)
    {
        Vector3 lookRotation = Quaternion.LookRotation(_lookDir).eulerAngles;
        Player.instance.transform.rotation = Quaternion.Euler(0f, lookRotation.y, 0f);
    }

}
