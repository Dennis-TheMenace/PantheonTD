using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spline : MonoBehaviour
{
    public Vector3[] points;
    public int pointCount;
    private LineRenderer line;

    // Start is called before the first frame update
    void Start()
    {
        pointCount = transform.childCount;
        points = new Vector3[pointCount];
        line = GetComponent<LineRenderer>();
        line.SetWidth(.6f, .6f);

        for( int i = 0; i < pointCount; i++)
        {
            points[i] = transform.GetChild(i).position;
        }
        
        for( int i = 0; i < pointCount; i++)
        {
            line.SetPosition(i, points[i]);
            //Debug.Log(points[i]);
        }      
    }

    // Update is called once per frame
    void Update()
    {
        if(pointCount > 1)
        {
            for( int i = 0; i < pointCount - 1; i++)
            {
                Debug.DrawLine(points[i], points[i+1], Color.red);
            }
        }
    }
}
