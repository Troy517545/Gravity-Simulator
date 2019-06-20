using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class starObject : MonoBehaviour
{
    starSystemObject starSystemScript;
    Star s;

    public VEC vol = new VEC(3);
    void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        starSystemScript = GameObject.Find("starSystemObject").GetComponent<starSystemObject>();
        s = new Star(transform.position[0], transform.position[1], transform.position[2], vol[0], vol[1], vol[2], 2E9);
        starSystemScript.addStarToSystem(s);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 posVec3 = new Vector3((float)s.pos[0], (float)s.pos[1], (float)s.pos[2]);
        transform.position = posVec3;

    }
}
