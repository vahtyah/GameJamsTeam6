using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //[SerializeField] float smoothTime;
    //Vector3 curVelo;
    Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - Player.instance.transform.position;
    }

    private void LateUpdate()
    {
        Vector3 targetPos = Player.instance.transform.position + offset;
        transform.position = targetPos;
        //transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref curVelo, smoothTime, Mathf.Infinity, Time.deltaTime) ;
    }
}
