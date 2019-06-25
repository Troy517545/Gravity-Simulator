using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUILayoutObject : MonoBehaviour
{
    public GameObject myPrefab;
    starSystemObject starSystemObjectScript;
    cameraMovement cameraMovementScript;

    public string newStarXStr = "-40.0";
    public string newStarYStr = "0.0";
    public string newStarZStr = "0.0";
    public string newStarVxStr = "0.0";
    public string newStarVyStr = "-0.1";
    public string newStarVzStr = "0.0";
    public string newStarMassStr = "1E9";

    private float  newStarX;
    private float newStarY;
    private float newStarZ;
    private float newStarVx;
    private float newStarVy;
    private float newStarVz;
    private float newStarMass;

    public string starInfoToDisplay = "";

    public bool displayStarInfo = false;

    private bool haveInvalidInput = false;

    // Start is called before the first frame update
    void Start()
    {
        starSystemObjectScript = GameObject.Find("starSystemObject").GetComponent<starSystemObject>();
        cameraMovementScript = GameObject.Find("Main Camera").GetComponent<cameraMovement>();

        newStarXStr = "0.0";
        newStarYStr = "0.0";
        newStarZStr = "10.0";
        newStarVxStr = "0.0";
        newStarVyStr = "0.0";
        newStarVzStr = "0.0";
        newStarMassStr = "1E9";

        displayStarInfo = false;

        float.TryParse(newStarXStr, out newStarX);
        float.TryParse(newStarYStr, out newStarY);
        float.TryParse(newStarZStr, out newStarZ);
        float.TryParse(newStarVxStr, out newStarVx);
        float.TryParse(newStarVyStr, out newStarVy);
        float.TryParse(newStarVzStr, out newStarVz);
        float.TryParse(newStarMassStr, out newStarMass);


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnGUI()
    {
        if(cameraMovementScript.escMenuActiveStatus == false & cameraMovementScript.cameraRotationLock == true)
        {
            haveInvalidInput = false;
            GUILayout.BeginHorizontal("box");
            GUILayout.Label("Pos x: ");
            newStarXStr = GUILayout.TextField(newStarXStr, 25, GUILayout.Width(60));
            StringToFloatCheckAndAssign(ref newStarXStr, ref newStarX);
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal("box");
            GUILayout.Label("Pos y: ");
            newStarYStr = GUILayout.TextField(newStarYStr, 25, GUILayout.Width(60));
            StringToFloatCheckAndAssign(ref newStarYStr, ref newStarY);
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal("box");
            GUILayout.Label("Pos z: ");
            newStarZStr = GUILayout.TextField(newStarZStr, 25, GUILayout.Width(60));
            StringToFloatCheckAndAssign(ref newStarZStr, ref newStarZ);
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal("box");
            GUILayout.Label("V x: ");
            newStarVxStr = GUILayout.TextField(newStarVxStr, 25, GUILayout.Width(60));
            StringToFloatCheckAndAssign(ref newStarVxStr, ref newStarVx);
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal("box");
            GUILayout.Label("V y: ");
            newStarVyStr = GUILayout.TextField(newStarVyStr, 25, GUILayout.Width(60));
            StringToFloatCheckAndAssign(ref newStarVyStr, ref newStarVy);
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal("box");
            GUILayout.Label("V z: ");
            newStarVzStr = GUILayout.TextField(newStarVzStr, 25, GUILayout.Width(60));
            StringToFloatCheckAndAssign(ref newStarVzStr, ref newStarVz);
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal("box");
            GUILayout.Label("Mass: ");
            newStarMassStr = GUILayout.TextField(newStarMassStr, 25, GUILayout.Width(60));
            StringToFloatCheckAndAssign(ref newStarMassStr, ref newStarMass);
            GUILayout.EndHorizontal();

            if (haveInvalidInput == true)
            {
                GUI.enabled = false;
            }
            if (GUILayout.Button("Add star"))
            {
                var newStar = Instantiate(myPrefab, new Vector3(float.Parse(newStarXStr, System.Globalization.NumberStyles.Float),
                                                                float.Parse(newStarYStr, System.Globalization.NumberStyles.Float),
                                                                float.Parse(newStarZStr, System.Globalization.NumberStyles.Float)), Quaternion.identity);

                double[] v = new double[3] { double.Parse(newStarVxStr, System.Globalization.NumberStyles.Float),
                                         double.Parse(newStarVyStr, System.Globalization.NumberStyles.Float),
                                         double.Parse(newStarVzStr, System.Globalization.NumberStyles.Float)};
                newStar.GetComponent<starObject>().vel = new VEC(3, v);
                newStar.GetComponent<starObject>().mass = float.Parse(newStarMassStr, System.Globalization.NumberStyles.Float);
                newStar.GetComponent<starObject>().lineRendererColor = Color.yellow;
            }
            if (haveInvalidInput == true)
            {
                GUI.enabled = true;
            }
            if (GUILayout.Button("Clear all stars"))
            {
                starSystemObjectScript.ClearAllStars(-1);
            }


            if (GUI.Button(new Rect(5, Screen.height - 57, 111, 23), "Start"))
            {
                if (starSystemObjectScript.systemRunning == false)
                {
                    starSystemObjectScript.StartSystem();
                    starSystemObjectScript.systemRunning = true;
                }
            }

            if (GUI.Button(new Rect(5, Screen.height - 30, 110, 23), "Pause"))
            {
                if (starSystemObjectScript.systemRunning == true)
                {
                    starSystemObjectScript.PauseSystem();
                    starSystemObjectScript.systemRunning = false;
                }
            }
        }
        if (cameraMovementScript.escMenuActiveStatus == false & displayStarInfo == true)
        {
            GUI.Label(new Rect(Screen.width - 190, Screen.height - 115, 190, 115), starInfoToDisplay);
        }
    }

    private void StringToFloatCheckAndAssign(ref string str, ref float a)
    {
        if(float.TryParse(str, out a))
        {
           
        }
        else
        {
            haveInvalidInput = true;
        }
    }
}
