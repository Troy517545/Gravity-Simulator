using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class starSystemObject : MonoBehaviour
{
    starSystem system = new starSystem();
    private float update;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        update += Time.deltaTime;

        if (update > 0.1f)
        {
            update -= 0.1f;
            system.update(0.5);
        }
    }

    public void addStarToSystem(Star a)
    {
        system.addStar(a);
        Debug.Log("Added star to system!");
    }
}
