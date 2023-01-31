using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Spline spline;
    public Enemy enemy;

    public int pointNumber;
    private float interpolateAmount;
    private GameObject pManager;

    // Start is called before the first frame update
    void Start()
    {
        pManager = GameObject.Find("PlayerManager");
        pointNumber = 0;
        PassParameters(spline.points, spline.pointCount);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(enemy.active == true)
        {
            if(pointNumber < spline.pointCount - 1)
            {  
                interpolateAmount = (interpolateAmount + (Time.deltaTime * enemy.movementSpeed));
                enemy.transform.position = Vector3.Lerp(spline.points[pointNumber], spline.points[pointNumber + 1], interpolateAmount);
                if(enemy.transform.position == spline.points[pointNumber + 1])
                {
                    pointNumber++;
                    interpolateAmount = 0;
                }
            } 

            if(enemy.transform.position == spline.points[spline.pointCount - 1])
            {
                // Debug.Log("Destroyed");
                enemy.active = false;
                enemy.transform.position = new Vector3(1000,0,0);
                //Lose life here
                pManager.GetComponent<PlayerResources>().TakeDamage(1);
            }
        }
    }

    public void PassParameters(Vector3[] splinePoints, int splinePointCount)
    {
        spline.pointCount = splinePointCount;
        spline.points = splinePoints;
    }
}
