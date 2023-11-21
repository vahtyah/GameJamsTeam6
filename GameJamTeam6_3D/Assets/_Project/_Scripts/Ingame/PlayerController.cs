using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Joystick))]
public class PlayerController : MonoBehaviour
{
    Rigidbody rb;
    [BoxGroup("Input Value")]
    public float xAxis, yAxis;
    Vector3 dir;
    [SerializeField] float moveSpeed;
    [SerializeField] float rotSpeed;
    public Joystick joystick;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void FixedUpdate()
    {
        MovePositon();
        MoveRotation();
    }
    public Vector3 Direction => dir;

    void Update()
    {
        GetInput();
    }
    void MovePositon()
    {
        Vector3 nextPos = rb.position;
        nextPos += dir * moveSpeed * Time.fixedDeltaTime;
        rb.MovePosition(nextPos);
    }
    void MoveRotation()
    {
        if (dir.sqrMagnitude <= .1f) return;
        var rot = Quaternion.LookRotation(dir, Vector3.up);
        transform.rotation = Quaternion.Slerp(rb.rotation, rot, rotSpeed * Time.fixedDeltaTime);
    }
    void GetInput()
    {
#if UNITY_EDITOR
        xAxis = Input.GetAxis("Horizontal");
        yAxis = Input.GetAxis("Vertical");
#else
        xAxis = joystick.Horizontal;
        yAxis = joystick.Vertical;
#endif
        dir = new Vector3(xAxis, 0, yAxis).normalized;
        dir = Quaternion.Euler(0, Camera.main.transform.eulerAngles.y, 0) * dir;
    }
}
