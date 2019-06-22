using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    public GameObject myPrefab;
    void awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        //var star_a = Instantiate(myPrefab, new Vector3(1.0f, 0.0f, 0.0f), Quaternion.identity);
        //var star_b = Instantiate(myPrefab, new Vector3(-1.0f, 0.0f, 0.0f), Quaternion.identity);
        //var star_c = Instantiate(myPrefab, new Vector3(-3f, 0.0f, 0.0f), Quaternion.identity);
        //var star_d = Instantiate(myPrefab, new Vector3(-19f, 0.0f, 0.0f), Quaternion.identity);

        //double[] v = new double[3] { -0.1, 0.15, 0 };
        //star_a.GetComponent<starObject>().vel = new VEC(3, v);
        //star_a.GetComponent<starObject>().mass = 1E9;
        //star_a.GetComponent<starObject>().lineRendererColor = Color.blue;

        //v = new double[3] { 0, -0.15, 0 };
        //star_b.GetComponent<starObject>().vel = new VEC(3, v);
        //star_b.GetComponent<starObject>().mass = 1E9;
        //star_b.GetComponent<starObject>().lineRendererColor = Color.green;

        //v = new double[3] { 0.0, 0.3, 0 };
        //star_c.GetComponent<starObject>().vel = new VEC(3, v);
        //star_c.GetComponent<starObject>().mass = 1E9;
        //star_c.GetComponent<starObject>().lineRendererColor = Color.red;

        //v = new double[3] { 0, -0.1, 0 };
        //star_d.GetComponent<starObject>().vel = new VEC(3, v);
        //star_d.GetComponent<starObject>().mass = 1E9;
        //star_d.GetComponent<starObject>().lineRendererColor = Color.gray;


        var star_a = Instantiate(myPrefab, new Vector3(5.0f, 0.0f, 0.0f), Quaternion.identity);
        var star_b = Instantiate(myPrefab, new Vector3(-5.0f, 0.0f, 0.0f), Quaternion.identity);
        //var star_c = Instantiate(myPrefab, new Vector3(10.0f, 0.0f, 10.0f), Quaternion.identity);

        double[] v = new double[3] { 0, 0.02583424, 0 };
        star_a.GetComponent<starObject>().vel = new VEC(3, v);
        star_a.GetComponent<starObject>().mass = 2E8;
        star_a.GetComponent<starObject>().lineRendererColor = Color.blue;

        v = new double[3] { 0, -0.02583424, 0 };
        star_b.GetComponent<starObject>().vel = new VEC(3, v);
        star_b.GetComponent<starObject>().mass = 2E8;
        star_b.GetComponent<starObject>().lineRendererColor = Color.green;

        //v = new double[3] { 0.0, 0.0, 0.04 };
        //star_c.GetComponent<starObject>().vel = new VEC(3, v);
        //star_c.GetComponent<starObject>().mass = 2E9;
        //star_c.GetComponent<starObject>().lineRendererColor = Color.cyan;


        //var star_a = Instantiate(myPrefab, new Vector3(5.0f, 0.0f, 0.0f), Quaternion.identity);
        //var star_b = Instantiate(myPrefab, new Vector3(-5.0f, 0.0f, 0.0f), Quaternion.identity);
        //var star_c = Instantiate(myPrefab, new Vector3(0.0f, 0.0f, 10f), Quaternion.identity);

        //double[] v = new double[3] { 0, 0.02583424, 0 };
        //star_a.GetComponent<starObject>().vel = new VEC(3, v);
        //star_a.GetComponent<starObject>().mass = 2E8;
        //star_a.GetComponent<starObject>().lineRendererColor = Color.blue;

        //v = new double[3] { 0, -0.02583424, 0 };
        //star_b.GetComponent<starObject>().vel = new VEC(3, v);
        //star_b.GetComponent<starObject>().mass = 2E8;
        //star_b.GetComponent<starObject>().lineRendererColor = Color.green;

        //v = new double[3] { 0, 0.0, -0.01 };
        //star_c.GetComponent<starObject>().vel = new VEC(3, v);
        //star_c.GetComponent<starObject>().mass = 1E9;
        //star_c.GetComponent<starObject>().lineRendererColor = Color.cyan;
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
