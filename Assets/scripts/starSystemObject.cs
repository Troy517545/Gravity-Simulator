using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using System.Threading;

public class starSystemObject : MonoBehaviour
{
    GUILayoutObject GUIObjectScript;
    cameraMovement cameraMovementScript;

    starSystem system;
    
    public bool systemRunning = true;

    public double h = 0.2;

    // Start is called before the first frame update
    void Start()
    {
        system = new starSystem();
        GUIObjectScript = GameObject.Find("GUIObject").GetComponent<GUILayoutObject>();
        cameraMovementScript = GameObject.Find("Main Camera").GetComponent<cameraMovement>();
        UpdateSystem();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddStarToSystem(Star a)
    {
        system.AddStar(a);
        Debug.Log("Added star to system!");
    }

    /*
     * validCondition = 0: keep orbit track
     * validCondition = -1: clear orbit track as well
     */
    public void ClearAllStars(int validCondition)
    {
        system.ClearAllStars(validCondition);
        GameObject[] allTrackObjects = GameObject.FindGameObjectsWithTag("trackObject");
        foreach (GameObject element in allTrackObjects)
        {
            Destroy(element);
        }
        GUIObjectScript.displayStarInfo = false;
    }

    public void StartSystem()
    {
        if (systemRunning == false)
        {
            system.StartSystem();
            systemRunning = true;
        }
    }

    public void PauseSystem()
    {
        if (systemRunning == true)
        {
            system.PauseSystem();
            systemRunning = false;
        }
    }
    

    public async Task UpdateSystem()
    {
        while (true)
        {
            system.UpdateSystem(h);
            await Task.Delay(1);
        }
    }

}
