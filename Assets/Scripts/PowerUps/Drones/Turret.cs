using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class Turret : MonoBehaviour
{
    public GameObject target;
    public PlaneManager plane;
    public float shootTimer = 0;
    public float targetDistance = 10f;
    public float rotationSpeed = 10f;

    // Start is called before the first frame update
    void Start()
    {

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
            plane.planeShooter.shootTurret(gameObject);
            shootTimer = 0;
        }
    }

    public void followTarget(){
        try{
            float rotation;
            if (target != gameObject){
                Vector3 objective = target.transform.position - transform.position;
                rotation = Vector3.Angle(objective, transform.right);
                var rotationR = Vector3.Angle(objective, transform.up);
                var rotationL = Vector3.Angle(objective, -transform.up);

                if(rotation > 5){
                rotation = 5;
                }

                if (rotationL < rotationR){
                    rotation = -rotation;
                }
            } else {
                rotation = rotationSpeed;
            }

            transform.Rotate(0,0,rotation);
        } catch(Exception e){
            target = gameObject;
            Debug.Log(e);
        }
        
        
    }

    public void searchTarget(){
        Debug.DrawRay(transform.position,transform.right * targetDistance,Color.red);
        var detected = SearchTarget.searchTarget(gameObject, targetDistance, transform.right).target;
        if (detected != gameObject){
            target = detected;
        } else if (target == null || Vector3.Distance(transform.position, target.transform.position) > targetDistance){
            target = gameObject;
        }
    }

    
}
