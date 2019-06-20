using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class cameraMovement : MonoBehaviour
{
    private bool cameraRotationLock = false;
    private bool cameraMovementLock = false;

    public float speedH = 3.0f;
    public float speedV = 3.0f;
    

    public float walkSpeed = 0.5f;


    private float yaw = -180.0f;
    private float pitch = 2.4f;

    void Update()
    {
        if (cameraRotationLock == false)
        {
            yaw += speedH * Input.GetAxis("Mouse X");
            pitch -= speedV * Input.GetAxis("Mouse Y");
            if (pitch > 85)
            {
                pitch = 85;
            }
            else if (pitch < -85)
            {
                pitch = -85;
            }
            transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);
        }
        

        if (Input.GetKeyDown("l"))
        {
            cameraRotationLock = (cameraRotationLock == false) ? true : false;
            cameraMovementLock = (cameraMovementLock == false) ? true : false;
        }

        if (cameraMovementLock == false)
        {
            if (Input.GetKey("w"))
            {
                transform.position += walkSpeed * (transform.forward).normalized;
            }
            if (Input.GetKey("s"))
            {
                transform.position -= walkSpeed * (transform.forward).normalized;
            }
            if (Input.GetKey("a"))
            {
                transform.position -= walkSpeed * (transform.right).normalized;
            }
            if (Input.GetKey("d"))
            {
                transform.position += walkSpeed * (transform.right).normalized;
            }
            if (Input.GetKey("left shift"))
            {
                transform.position -= walkSpeed * (Vector3.up).normalized;
            }
            if (Input.GetKey("space"))
            {
                transform.position += walkSpeed * (Vector3.up).normalized;
            }
        }
    }
}
