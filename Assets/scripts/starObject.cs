using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class starObject : MonoBehaviour
{
    starSystemObject starSystemScript;
    Star s;
    LineRenderer lineRenderer;
    public int maxLengthOfLineRenderer = 400;
    private int nowLineRendererIndex = 0;
    public Color lineRendererColor;
    public double mass = 2E9;
    public GameObject trackObjectPrefab;


    public VEC vol = new VEC(3);
    void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        starSystemScript = GameObject.Find("starSystemObject").GetComponent<starSystemObject>();
        s = new Star(transform.position[0], transform.position[1], transform.position[2], vol[0], vol[1], vol[2], mass);
        starSystemScript.addStarToSystem(s);
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.widthMultiplier = 0.1f;
        lineRenderer.startColor = lineRendererColor;
        lineRenderer.endColor = Color.white;

        lineRenderer.positionCount = nowLineRendererIndex;

    }

    // Update is called once per frame
    void Update()
    {
        if (s.valid == 0)
        {
            var track = Instantiate(trackObjectPrefab);
            LineRenderer trackLineRenderer = track.GetComponent<LineRenderer>();
            trackLineRenderer.widthMultiplier = 0.1f;
            trackLineRenderer.startColor = lineRendererColor;
            trackLineRenderer.endColor = Color.white;

            trackLineRenderer.positionCount = lineRenderer.positionCount;


            for (int i=0;i< lineRenderer.positionCount; i++)
            {
                trackLineRenderer.SetPosition(i, lineRenderer.GetPosition(i));
            }

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
    }
}
