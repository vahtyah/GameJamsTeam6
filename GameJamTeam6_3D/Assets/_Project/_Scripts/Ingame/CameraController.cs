using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] PlayerController player;
    [SerializeField] float smoothTime;
    Vector3 curVelo;
    Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        Vector3 targetPos = player.transform.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref curVelo, smoothTime, Mathf.Infinity, Time.deltaTime) ;
    }
}
