using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class starObject : MonoBehaviour
{
    starSystemObject starSystemScript;
    GUILayoutObject GUIObjectScript;
    public GameObject trackObjectPrefab;
    LineRenderer lineRenderer;

    Star s;
    
    public int maxLengthOfLineRenderer = 400;
    private int nowLineRendererIndex = 0;
    public Color lineRendererColor;
    public double mass = 1E9;

    public VEC vel = new VEC(3);

    public bool selectedToDisplayInfo = false;
    void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        starSystemScript = GameObject.Find("starSystemObject").GetComponent<starSystemObject>();
        GUIObjectScript = GameObject.Find("GUIObject").GetComponent<GUILayoutObject>();

        s = new Star(transform.position[0], transform.position[1], transform.position[2], vel[0], vel[1], vel[2], mass);
        starSystemScript.AddStarToSystem(s);

        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.widthMultiplier = 0.1f;
        lineRenderer.positionCount = nowLineRendererIndex;
        
        Gradient gradient = new Gradient();
        gradient.SetKeys(
            new GradientColorKey[] { new GradientColorKey(lineRendererColor, 0.0f), new GradientColorKey(Color.Lerp(lineRendererColor, Color.white, 0.5f), 0.9f), new GradientColorKey(Color.white, 1.0f) },
            new GradientAlphaKey[] { new GradientAlphaKey(lineRendererColor.a, 0.0f), new GradientAlphaKey(lineRendererColor.a, 1.0f) }
        );
        lineRenderer.colorGradient = gradient;

        selectedToDisplayInfo = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(s.valid == -1)
        {
            Destroy(gameObject);
        }
        else if (s.valid == 0)
        {
            var track = Instantiate(trackObjectPrefab);
            LineRenderer trackLineRenderer = track.GetComponent<LineRenderer>();
            trackLineRenderer.widthMultiplier = 0.1f;
            trackLineRenderer.startColor = lineRendererColor;
            trackLineRenderer.endColor = Color.white;

            trackLineRenderer.positionCount = lineRenderer.positionCount;

            Vector3[] positions = new Vector3[lineRenderer.positionCount];
            lineRenderer.GetPositions(positions);
            trackLineRenderer.SetPositions(positions);

            Gradient gradient = new Gradient();
            gradient.SetKeys(
                new GradientColorKey[] { new GradientColorKey(lineRendererColor, 0.0f), new GradientColorKey(Color.Lerp(lineRendererColor, Color.white, 0.5f), 0.9f), new GradientColorKey(Color.white, 1.0f) },
                new GradientAlphaKey[] { new GradientAlphaKey(lineRendererColor.a, 0.0f), new GradientAlphaKey(lineRendererColor.a, 1.0f) }
            );
            trackLineRenderer.colorGradient = gradient;

            Destroy(gameObject);
        }
        else
        {
            Vector3 posVec3 = new Vector3((float)s.pos[0], (float)s.pos[1], (float)s.pos[2]);
            if (transform.position != posVec3)
            {
                transform.position = posVec3;
                if (nowLineRendererIndex < maxLengthOfLineRenderer)
                {
                    lineRenderer.positionCount = nowLineRendererIndex + 1;

                    lineRenderer.SetPosition(nowLineRendererIndex, posVec3);
                    nowLineRendererIndex++;
                }
                else
                {
                    lineRenderer.Simplify(0.02f);
                    // Debug.Log("Line reduced from " + nowLineRendererIndex + " to " + lineRenderer.positionCount);
                    nowLineRendererIndex = lineRenderer.positionCount;
                    if (maxLengthOfLineRenderer < 5000)
                    {
                        maxLengthOfLineRenderer += 100;
                    }
                }
            }
        }
        if(selectedToDisplayInfo == true)
        {
            GUIObjectScript.starInfoToDisplay =
                "Pos x: " + s.pos[0].ToString() + "\n" +
                "Pos y: " + s.pos[1].ToString() + "\n" +
                "Pos x: " + s.pos[2].ToString() + "\n" +
                "V x: " + s.vel[0].ToString() + "\n" +
                "V y: " + s.vel[1].ToString() + "\n" +
                "V z: " + s.vel[2].ToString() + "\n" +
                "Mass: " + s.mass.ToString() + "\n";
        }
    }

    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (selectedToDisplayInfo == true)
            {
                selectedToDisplayInfo = false;
                GUIObjectScript.displayStarInfo = false;
            }
            else
            {
                selectedToDisplayInfo = true;
                GUIObjectScript.displayStarInfo = true;
                GameObject[] allStarObjects = GameObject.FindGameObjectsWithTag("starObject");
                foreach (GameObject element in allStarObjects)
                {
                    if(element != gameObject) // select all other star objects
                    {
                        if(element.GetComponent<starObject>().selectedToDisplayInfo == true)
                        {
                            element.GetComponent<starObject>().selectedToDisplayInfo = false; // should only allow one star's info to be displayed 
                        }
                    }
                }
            }
        }
    }

    private double Distance(VEC a, VEC b)
    {
        double dist = 0;
        for (int i = 0; i < 3; i++)
        {
            dist += Math.Pow(a[i] - b[i], 2);
        }
        return Math.Sqrt(dist);
    }
}
