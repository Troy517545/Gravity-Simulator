using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class cameraMovement : MonoBehaviour
{
    public float speedH = 3.0f;
    public float speedV = 3.0f;


    public float walkSpeed = 1f;


    private float yaw = 0.0f;
    private float pitch = 0.0f;

    void Update()
    {
        yaw += speedH * Input.GetAxis("Mouse X");
        pitch -= speedV * Input.GetAxis("Mouse Y");

        if(pitch > 85)
        {
            pitch = 85;
        }
        else if(pitch < -85)
        {
            pitch = -85;
        }
        transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);


        if (Input.GetKey("w"))
        {
            transform.position -= walkSpeed * (Vector3.Cross(Vector3.up, transform.right).normalized);
        }
        if (Input.GetKey("s"))
        {
            transform.position += walkSpeed * (Vector3.Cross(Vector3.up, transform.right).normalized);
        }
        if (Input.GetKey("a"))
        {
            transform.position -= walkSpeed * (transform.right).normalized;
        }
        if (Input.GetKey("d"))
        {
            transform.position += walkSpeed * (transform.right).normalized;
        }
        if (Input.GetKey("left ctrl"))
        {
            transform.position -= walkSpeed * (transform.up).normalized;
        }
        if (Input.GetKey("left shift"))
        {
            transform.position += walkSpeed * (transform.up).normalized;
        }
    }
}
