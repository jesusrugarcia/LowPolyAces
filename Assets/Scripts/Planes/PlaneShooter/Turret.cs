using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class Turret : MonoBehaviour
{
    public TargetResult[] results;
    public GameObject target;
    public PlaneManager plane;
    public float shootTimer = 0;
    public float targetDistance = 15f;

    // Start is called before the first frame update
    void Start()
    {
        results = new TargetResult[4];
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        searchTarget();
        followTarget();
        shoot();
    }

    public void shoot(){
        shootTimer += Time.deltaTime;
        if(shootTimer >= plane.stats.turretShootSpeed){
            plane.planeShooter.shoot(gameObject);
            shootTimer = 0;
        }
    }

    public void followTarget(){
        try{
            Vector3 objective = target.transform.position - transform.position;
            var rotation = Vector3.Angle(objective, transform.right);
            var rotationR = Vector3.Angle(objective, transform.up);
            var rotationL = Vector3.Angle(objective, -transform.up);

            if(rotation > 5){
             rotation = 5;
            }

            if (rotationL < rotationR){
                rotation = -rotation;
            }

            transform.Rotate(0,0,rotation);
        } catch(Exception e){
            target = gameObject;
            Debug.Log(e);
        }
        
        
    }

    public void searchTarget(){
        results[0] = SearchTarget.searchTarget(gameObject, targetDistance, transform.right);
        results[1] = SearchTarget.searchTarget(gameObject, targetDistance, -transform.right);
        results[2] = SearchTarget.searchTarget(gameObject, targetDistance, transform.forward);
        results[3] = SearchTarget.searchTarget(gameObject, targetDistance, -transform.forward);
        Debug.DrawRay(transform.position, transform.right * 10 , Color.red);
        Debug.DrawRay(transform.position, -transform.right * 10 , Color.green);
        Debug.DrawRay(transform.position, transform.up * 10 , Color.magenta);
        Debug.DrawRay(transform.position, -transform.up * 10 , Color.blue);

        var best = SearchTarget.CompareTargetResults(results);
        if(results[best].distance < Vector3.Distance(transform.position, target.transform.position) || target != gameObject){
            target = results[best].target;
        }
        

    }

    
}
