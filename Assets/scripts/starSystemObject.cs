using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using System.Threading;

public class starSystemObject : MonoBehaviour
{
    starSystem system = new starSystem();
    private float update;

    public string newStarX = "-40.0";
    public string newStarY = "0.0";
    public string newStarZ = "0.0";
    public string newStarVx = "0.0";
    public string newStarVy = "-0.1";
    public string newStarVz = "0.0";
    public string newStarMass = "1E9";

    public GameObject myPrefab;


    // Start is called before the first frame update
    void Start()
    {
        newStarX = "0.0";
        newStarY = "0.0";
        newStarZ = "10.0";
        newStarVx = "0.0";
        newStarVy = "0.0";
        newStarVz = "0.0";
        newStarMass = "1E9";


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

    public async Task updateSystem()
    {
        while (true)
        {
            system.update(0.1);
            await Task.Delay(1);
        }
    }

    void OnGUI()
    {
        GUILayout.BeginHorizontal("box");
        GUILayout.Label("Pos x: ");
        newStarX = GUILayout.TextField(newStarX, 25, GUILayout.Width(60));
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal("box");
        GUILayout.Label("Pos y: ");
        newStarY = GUILayout.TextField(newStarY, 25, GUILayout.Width(60));
        GUILayout.EndHorizontal();


        GUILayout.BeginHorizontal("box");
        GUILayout.Label("Pos z: ");
        newStarZ = GUILayout.TextField(newStarZ, 25, GUILayout.Width(60));
        GUILayout.EndHorizontal();


        GUILayout.BeginHorizontal("box");
        GUILayout.Label("Vel x: ");
        newStarVx = GUILayout.TextField(newStarVx, 25, GUILayout.Width(60));
        GUILayout.EndHorizontal();


        GUILayout.BeginHorizontal("box");
        GUILayout.Label("Vel y: ");
        newStarVy = GUILayout.TextField(newStarVy, 25, GUILayout.Width(60));
        GUILayout.EndHorizontal();


        GUILayout.BeginHorizontal("box");
        GUILayout.Label("Vel z: ");
        newStarVz = GUILayout.TextField(newStarVz, 25, GUILayout.Width(60));
        GUILayout.EndHorizontal();


        GUILayout.BeginHorizontal("box");
        GUILayout.Label("Mass: ");
        newStarMass = GUILayout.TextField(newStarMass, 25, GUILayout.Width(60));
        GUILayout.EndHorizontal();


        if (GUILayout.Button("Add star"))
        {
            var newStar = Instantiate(myPrefab, new Vector3(float.Parse(newStarX, System.Globalization.NumberStyles.Float), 
                                                            float.Parse(newStarY, System.Globalization.NumberStyles.Float), 
                                                            float.Parse(newStarZ, System.Globalization.NumberStyles.Float)), Quaternion.identity);

            double[] v = new double[3] { double.Parse(newStarVx, System.Globalization.NumberStyles.Float),
                                         double.Parse(newStarVy, System.Globalization.NumberStyles.Float),
                                         double.Parse(newStarVz, System.Globalization.NumberStyles.Float)};
            newStar.GetComponent<starObject>().vel = new VEC(3, v);
            newStar.GetComponent<starObject>().mass = float.Parse(newStarMass, System.Globalization.NumberStyles.Float);
            newStar.GetComponent<starObject>().lineRendererColor = Color.yellow;
            
        }

        if (GUILayout.Button("Clear all stars"))
        {
            system.clearAllStars(-1);
        }
    }

}
