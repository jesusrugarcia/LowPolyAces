using UnityEngine;
using System.Collections.Generic;
[RequireComponent(typeof(LineRenderer))]
public class Bezier : MonoBehaviour
{
    public Vector3[] controlPoints;
    public LineRenderer lineRenderer;
    public int numberOfPoints = 25;
    
        
    void Start()
    {
        
    }
    
    public void DrawCurve()
    {
        if (numberOfPoints > 0)
   		{
    			lineRenderer.positionCount = numberOfPoints;
		}

		// set points of quadratic Bezier curve
		Vector3 p0 = controlPoints[0];
		Vector3 p1 =controlPoints[1];
		Vector3 p2 = controlPoints[2];
		float t;
		Vector3 position;
		for(int i = 0; i < numberOfPoints; i++)
		{
			t = i / (numberOfPoints - 1.0f);
			position = (1.0f - t) * (1.0f - t) * p0 
			+ 2.0f * (1.0f - t) * t * p1 + t * t * p2;
			lineRenderer.SetPosition(i, position);
		}
    }
        
}