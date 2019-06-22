using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    public GameObject myPrefab;

    int initialConditions = 1;
    private double G = 6.67408E-11;


    void awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        if (initialConditions == 1)
        {
            Color color;
            color.a = 0.75f;
            float scale = 10f;
            float z_vel = 0.01f;
            var star_a = Instantiate(myPrefab, new Vector3(scale * 0.98000436f, scale * -0.24308753f, 0.0f), Quaternion.identity);
            var star_b = Instantiate(myPrefab, new Vector3(scale * -0.97000436f, scale * 0.24308753f, 0.0f), Quaternion.identity);
            var star_c = Instantiate(myPrefab, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity);

            double[] v = new double[3] {0.93240737 / 2.0, 0.86473146 / 2.0, z_vel };
            star_a.GetComponent<starObject>().vel = new VEC(3, v);
            star_a.GetComponent<starObject>().mass = scale / G;
            color = Color.blue;
            color.a = 0.75f;
            star_a.GetComponent<starObject>().lineRendererColor = color;

            v = new double[3] {0.93240737 / 2.0, 0.86473146 / 2.0, z_vel };
            star_b.GetComponent<starObject>().vel = new VEC(3, v);
            star_b.GetComponent<starObject>().mass = scale / G;
            color = Color.green;
            color.a = 0.75f;
            star_b.GetComponent<starObject>().lineRendererColor = color;

            v = new double[3] {-0.93240737, -0.86473146 , z_vel };
            star_c.GetComponent<starObject>().vel = new VEC(3, v);
            star_c.GetComponent<starObject>().mass =  scale / G;
            color = Color.red;
            color.a = 0.75f;
            star_c.GetComponent<starObject>().lineRendererColor = color;
            return;
        }
        else
        {
            Color color;
            color.a = 0.75f;

            var star_a = Instantiate(myPrefab, new Vector3(1.0f, 0.0f, 0.0f), Quaternion.identity);
            var star_b = Instantiate(myPrefab, new Vector3(-1.0f, 0.0f, 0.0f), Quaternion.identity);
            var star_c = Instantiate(myPrefab, new Vector3(-3f, 0.0f, 0.0f), Quaternion.identity);
            //var star_d = Instantiate(myPrefab, new Vector3(19f, 0.0f, 0.0f), Quaternion.identity);

            double[] v = new double[3] { 0, 0.2 - 4.0 / 30.0, 0.005 };
            star_a.GetComponent<starObject>().vel = new VEC(3, v);
            star_a.GetComponent<starObject>().mass = 2E9;
            color = Color.blue;
            color.a = 0.75f;
            star_a.GetComponent<starObject>().lineRendererColor = color;

            v = new double[3] { 0, -0.2 - 4.0 / 30.0, 0.005 };
            star_b.GetComponent<starObject>().vel = new VEC(3, v);
            star_b.GetComponent<starObject>().mass = 2E9;
            color = Color.green;
            color.a = 0.75f;
            star_b.GetComponent<starObject>().lineRendererColor = color;

            v = new double[3] { 0.0, 0.4 - 4.0 / 30.0, 0.005 };
            star_c.GetComponent<starObject>().vel = new VEC(3, v);
            star_c.GetComponent<starObject>().mass = 2E9;
            color = Color.red;
            color.a = 0.75f;
            star_c.GetComponent<starObject>().lineRendererColor = color;


            //v = new double[3] { 0, 0.18, 0 };
            //star_d.GetComponent<starObject>().vel = new VEC(3, v);
            //star_d.GetComponent<starObject>().mass = 2E9;
            //star_d.GetComponent<starObject>().lineRendererColor = Color.gray;


            //var star_a = Instantiate(myPrefab, new Vector3(5.0f, 0.0f, 0.0f), Quaternion.identity);
            //var star_b = Instantiate(myPrefab, new Vector3(-5.0f, 0.0f, 0.0f), Quaternion.identity);
            ////var star_c = Instantiate(myPrefab, new Vector3(10.0f, 0.0f, 10.0f), Quaternion.identity);

            //double[] v = new double[3] { 0, 0.02583424, 0 };
            //star_a.GetComponent<starObject>().vel = new VEC(3, v);
            //star_a.GetComponent<starObject>().mass = 2E8;
            //star_a.GetComponent<starObject>().lineRendererColor = Color.blue;

            //v = new double[3] { 0, -0.02583424, 0 };
            //star_b.GetComponent<starObject>().vel = new VEC(3, v);
            //star_b.GetComponent<starObject>().mass = 2E8;
            //star_b.GetComponent<starObject>().lineRendererColor = Color.green;

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
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
