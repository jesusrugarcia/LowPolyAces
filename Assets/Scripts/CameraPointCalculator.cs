using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPointCalculator : MonoBehaviour
{
    public GameController controller;
    public float timer = 0;
    public float timeToIncrease = 5;
    public float distanceIncrease = 0.1f;

    void calculateBoundariesDebug()
    {
        Camera camera = GetComponent<Camera>();
        Vector3 p = camera.ViewportToWorldPoint(new Vector3(1, 0.5f, transform.position.y));
        Debug.DrawRay(Vector3.zero, p - Vector3.zero, Color.magenta, 0.1f);
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(p, 0.1F);
        Vector3 q = camera.ViewportToWorldPoint(new Vector3(0.5f, 1, transform.position.y));
        Debug.DrawRay(Vector3.zero, q - Vector3.zero, Color.green, 0.1f);
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(q, 0.1F);
    }

    void OnDrawGizmosSelected(){
        calculateBoundariesDebug();
    }

    void Start()
    {
        calculateBoundaries();
    }

    void FixedUpdate()
    {
        timer += Time.deltaTime;
        if (timer >= timeToIncrease){
            timer = 0;
            transform.position = new Vector3(0,transform.position.y + distanceIncrease, 0);
            calculateBoundaries();
        }
    }

    public void calculateBoundaries(){
        Camera camera = GetComponent<Camera>();
        Vector3 p = camera.ViewportToWorldPoint(new Vector3(1, 0.5f, transform.position.y));
        Debug.Log("max: " + p.x);
        controller.max = p.x;
        Vector3 q = camera.ViewportToWorldPoint(new Vector3(0.5f, 1, transform.position.y));
        controller.maz = q.z;
        Debug.Log("maz: " + q.z);
    }
}
