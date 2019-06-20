using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    public GameObject myPrefab;

    // Start is called before the first frame update
    void Start()
    {
        var star_a = Instantiate(myPrefab, new Vector3(1.0f, 0.0f, 0.0f), Quaternion.identity);
        var star_b = Instantiate(myPrefab, new Vector3(-1.0f, 0.0f, 0.0f), Quaternion.identity);
        var star_c = Instantiate(myPrefab, new Vector3(-3f, 0.0f, 0.0f), Quaternion.identity);
        
        double[] v = new double[3] { 0, 0.1, 0};
        star_a.GetComponent<starObject>().vol = new VEC(3, v);
        v = new double[3] { 0, -0.3, 0 };
        star_b.GetComponent<starObject>().vol = new VEC(3, v);
        v = new double[3] { 0, 0.2, 0 };
        star_c.GetComponent<starObject>().vol = new VEC(3, v);
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
