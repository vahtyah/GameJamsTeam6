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
        Move();
        Look();
    }

    void Move()
    {
        Vector3 normalizedMoveInput = new Vector3(InputHandler.instance.HorizontalMovement(), InputHandler.instance.VerticalMovement());
        float moveSpeed = speed * Time.fixedDeltaTime;
        Player.instance.GetRb().velocity =
            new Vector3(normalizedMoveInput.x * moveSpeed, 0, normalizedMoveInput.z * moveSpeed);
    }

    void Look()
    {

        Vector3 mousePos = Camera.main.ScreenToViewportPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, cameraDistance));
        Vector3 lookDir = mousePos - Player.instance.transform.position;
        Vector3 lookRotation = Quaternion.LookRotation(lookDir).eulerAngles;
        Player.instance.transform.rotation = Quaternion.Euler(0f, lookRotation.y, 0f);
    }

}
