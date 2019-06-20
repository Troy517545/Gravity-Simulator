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
        //var star_b = Instantiate(myPrefab, new Vector3(-10.0f, 0.0f, 0.0f), Quaternion.identity);
        var star_c = Instantiate(myPrefab, new Vector3(-3f, 0.0f, 0.0f), Quaternion.identity);
        var star_d = Instantiate(myPrefab, new Vector3(19f, 0.0f, 0.0f), Quaternion.identity);

        double[] v = new double[3] { 0, 0.2 - 4.0 / 30.0, 0 };
        // v = new double[3] { 0, 0.0, 0 };
        star_a.GetComponent<starObject>().vol = new VEC(3, v);
        star_a.GetComponent<starObject>().mass = 2E9;
        star_a.GetComponent<starObject>().lineRendererColor = Color.blue;

        v = new double[3] { 0, -0.2 - 4.0 / 30.0, 0 };
        star_b.GetComponent<starObject>().vol = new VEC(3, v);
        star_b.GetComponent<starObject>().mass = 2E9;
        star_b.GetComponent<starObject>().lineRendererColor = Color.green;

        v = new double[3] { 0, 0.4 - 4.0 / 30.0, 0 };
        star_c.GetComponent<starObject>().vol = new VEC(3, v);
        star_c.GetComponent<starObject>().lineRendererColor = Color.red;

        v = new double[3] { 0, 0.18, 0 };
        star_d.GetComponent<starObject>().vol = new VEC(3, v);
        star_d.GetComponent<starObject>().lineRendererColor = Color.gray;

    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
