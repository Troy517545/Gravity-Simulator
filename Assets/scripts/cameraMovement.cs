using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class cameraMovement : MonoBehaviour
{
    public GameObject escMenu;
    starSystemObject starSystemObjectScript;

    public bool escMenuActiveStatus = false;

    public bool cameraRotationLock = false;
    private bool cameraMovementLock = false;
    private bool slowWalkCondition = false;

    public float speedH = 3.0f;
    public float speedV = 3.0f;
    public float normalWalkSpeed = 0.5f;
    public float slowWalkSpeed = 0.2f;

    private float yaw = -180.0f;
    private float pitch = 2.4f;
    private float smooth_yaw;
    private float smooth_pitch;
    private float nowWalkSpeed;

    public bool systemRunningStatusBeforeMenuOpened = true;

    void Start()
    {
        starSystemObjectScript = GameObject.Find("starSystemObject").GetComponent<starSystemObject>();
 
        nowWalkSpeed = normalWalkSpeed;
    }

    void Update()
    {
        if (cameraRotationLock == false & escMenuActiveStatus == false)
        {
            yaw += speedH * Input.GetAxis("Mouse X");
            smooth_yaw = yaw;
            pitch -= speedV * Input.GetAxis("Mouse Y");
            smooth_pitch = pitch;
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
        else if(escMenuActiveStatus == true)
        {

            if (Input.mousePosition.x <= Screen.width * 0.99 & Input.mousePosition.x >= Screen.width * 0.01)
            {
                yaw += 0.04f * Input.GetAxis("Mouse X");
            }

            if (Input.mousePosition.y <= Screen.height * 0.99 & Input.mousePosition.y >= Screen.height * 0.01)
            {
                pitch -= 0.04f * Input.GetAxis("Mouse Y");
            }
                
            if (pitch > 85)
            {
                pitch = 85;
            }
            else if (pitch < -85)
            {
                pitch = -85;
            }
            smooth_pitch = Mathf.Lerp(smooth_pitch, pitch, 0.1f);
            smooth_yaw = Mathf.Lerp(smooth_yaw, yaw, 0.1f);
            transform.eulerAngles = new Vector3(smooth_pitch, smooth_yaw, 0.0f);
        }

        if (Input.GetKeyDown("escape"))
        {
            if(escMenuActiveStatus == true)
            {
                if(systemRunningStatusBeforeMenuOpened == true)
                {
                    starSystemObjectScript.StartSystem();
                }
                escMenuActiveStatus = false;
                escMenu.SetActive(false);
            }
            else
            {
                systemRunningStatusBeforeMenuOpened = starSystemObjectScript.systemRunning;
                starSystemObjectScript.PauseSystem();
                escMenuActiveStatus = true;
                escMenu.SetActive(true);
            }
        }

        if (Input.GetKeyDown("l"))
        {
            cameraRotationLock = (cameraRotationLock == false) ? true : false;
            cameraMovementLock = (cameraMovementLock == false) ? true : false;
        }

        if (cameraMovementLock == false & escMenuActiveStatus == false)
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

#if !UNITY_EDITOR
            if(Input.GetKeyDown("left alt"))
            {
                slowWalkCondition = true;
                nowWalkSpeed = slowWalkSpeed;
            }
            if (Input.GetKeyUp("left alt"))
            {
                slowWalkCondition = false;
                nowWalkSpeed = normalWalkSpeed;
            }
#endif
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
