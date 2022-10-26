using System;
using UnityEngine;

public class CameraPointCalculator : MonoBehaviour
{
    public GameController controller;
    public float timer = 0;
    public float timeToIncrease = 5;
    public float distanceIncrease = 0.1f;
    public float x;
    public float z;

    public bool localTerrain = true;
    public float realDistance = -900;

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
        if (timer >= timeToIncrease && localTerrain){
            timer = 0;
            transform.position = new Vector3(0,transform.position.y + distanceIncrease, 0);
            calculateBoundaries();
        }
    }

    public void calculateBoundaries(){
        Camera camera = GetComponent<Camera>();
        Vector3 p;
        Vector3 q;
        if(localTerrain){
            p = camera.ViewportToWorldPoint(new Vector3(1, 0.5f, transform.position.y));
            q = camera.ViewportToWorldPoint(new Vector3(0.5f, 1, transform.position.y));
        } else {
            p = camera.ViewportToWorldPoint(new Vector3(1, 0.5f, realDistance));
            q = camera.ViewportToWorldPoint(new Vector3(0.5f, 1, realDistance));
        }
        

        //Debug.Log("max: " + p.x);
        //Debug.Log("maz: " + q.z);
        x = p.x;
        z = q.z;

        try{
            controller.max = p.x;
            controller.maz = q.z;
        } catch (Exception e){
            Debug.Log(e);
        }
        
        
    }
}
