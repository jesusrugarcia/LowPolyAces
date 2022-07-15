using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControlHard : PlaneMovement
{
    public Transform objective;
    public float time = 0;
    
    void Update()
    {
        
    }

    void FixedUpdate(){
        accelerate();
        move();
        checkInScreen();

        time += Time.deltaTime;
        if(time >= plane.stats.timeToRotate){
            setObjective();
            calculateRotation();
            checkRotation();
            time = 0;
        }
        
    }

    public void checkRotation(){
        if( plane.stats.rotation < -plane.stats.maxRotation){
            plane.stats.rotation = -plane.stats.maxRotation;
        } else if (plane.stats.rotation > plane.stats.maxRotation){
            plane.stats.rotation = plane.stats.maxRotation;
        }
    }

     public void setObjective(){
        var min = 9999999f;
        var distance = 0f;
        GameObject otherPlane;
        for(int i= 0; i < plane.controller.players.Length; i++){
            otherPlane = plane.controller.players[i];
            distance = Vector3.Distance(plane.transform.position, otherPlane.transform.position);
            if(distance < min && otherPlane.GetComponent<PlaneStats>().health > 0){
                objective = otherPlane.transform;
            }
        }
    }

    public void calculateRotation(){
        Vector3 target = objective.position - transform.position;
        plane.stats.rotation = Vector3.Angle(target, transform.right)/plane.stats.rotation;
        var rotationR = Vector3.Angle(target, transform.up);
        var rotationL = Vector3.Angle(target, -transform.up);

        if (rotationL < rotationR){
            plane.stats.rotation = -plane.stats.rotation;
        }
        //Debug.DrawRay(transform.position, target , Color.red);
        //Debug.DrawRay(transform.position, transform.right * 10 , Color.green);
        //Debug.DrawRay(transform.position, transform.up * 10 , Color.magenta);
        //Debug.DrawRay(transform.position, -transform.up * 10 , Color.blue);
    }


}
