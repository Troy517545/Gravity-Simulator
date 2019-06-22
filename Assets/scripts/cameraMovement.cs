using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class cameraMovement : MonoBehaviour
{
    private bool cameraRotationLock = false;
    private bool cameraMovementLock = false;
    private bool slowWalkCondition = false;
    private float nowWalkSpeed;

    public float speedH = 3.0f;
    public float speedV = 3.0f;
    

    public float normalWalkSpeed = 0.5f;
    public float slowWalkSpeed = 0.2f;


    private float yaw = -180.0f;
    private float pitch = 2.4f;

    void Start()
    {
        nowWalkSpeed = normalWalkSpeed;
    }

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
                transform.position += nowWalkSpeed * (transform.forward).normalized;
            }
            if (Input.GetKey("s"))
            {
                transform.position -= nowWalkSpeed * (transform.forward).normalized;
            }
            if (Input.GetKey("a"))
            {
                transform.position -= nowWalkSpeed * (transform.right).normalized;
            }
            if (Input.GetKey("d"))
            {
                transform.position += nowWalkSpeed * (transform.right).normalized;
            }
            if (Input.GetKey("left shift"))
            {
                transform.position -= nowWalkSpeed * (Vector3.up).normalized;
            }
            if (Input.GetKey("space"))
            {
                transform.position += nowWalkSpeed * (Vector3.up).normalized;
            }
            //if(Input.GetKeyDown("left alt"))
            //{
            //    slowWalkCondition = true;
            //    nowWalkSpeed = slowWalkSpeed;
            //}
            //if (Input.GetKeyUp("left alt"))
            //{
            //    slowWalkCondition = false;
            //    nowWalkSpeed = normalWalkSpeed;
            //}
            if (Input.GetKeyDown("c"))
            {
                slowWalkCondition = (slowWalkCondition == true) ? false : true;
                if(slowWalkCondition == true)
                {
                    nowWalkSpeed = slowWalkSpeed;
                }
                else
                {
                    nowWalkSpeed = normalWalkSpeed;
                }
            }
        }
    }
}
