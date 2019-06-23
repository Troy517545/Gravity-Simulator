using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using System.Threading;

public class starSystemObject : MonoBehaviour
{
    GUILayoutObject GUIObjectScript;
    cameraMovement cameraMovementScript;

    starSystem system = new starSystem();
    
    public bool systemRunning = true;

    public double h = 0.2;

    // Start is called before the first frame update
    void Start()
    {
        GUIObjectScript = GameObject.Find("GUIObject").GetComponent<GUILayoutObject>();
        cameraMovementScript = GameObject.Find("Main Camera").GetComponent<cameraMovement>();
        updateSystem();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void addStarToSystem(Star a)
    {
        system.addStar(a);
        Debug.Log("Added star to system!");
    }

    /*
     * validCondition = 0: keep track
     * validCondition = -1: clear track as well
     */
    public void clearAllStars(int validCondition)
    {
        system.clearAllStars(validCondition);
        GameObject[] allTrackObjects = GameObject.FindGameObjectsWithTag("trackObject");
        foreach (GameObject element in allTrackObjects)
        {
            Destroy(element);
        }
        GUIObjectScript.displayStarInfo = false;
    }

    public void startSystem()
    {
        if (systemRunning == false)
        {
            system.startSystem();
            systemRunning = true;
        }
    }

    public void pauseSystem()
    {
        if (systemRunning == true)
        {
            system.pauseSystem();
            systemRunning = false;
        }
    }
    

public async Task updateSystem()
    {
        while (true)
        {
            system.update(h);
            await Task.Delay(1);
        }
    }
}
