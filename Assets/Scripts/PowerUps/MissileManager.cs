using System;
using System.Collections.Generic;
using UnityEngine;

public class MissileManager : MonoBehaviour
{
    public PlaneManager plane;
    public TeamManager teamManager;
    public GameObject target;

    public float targetDistance = 1f;
    public float speed = 10f;

    private void Start() {
        teamManager.team = plane.teamManager.team;
        target = this.gameObject;
        targetDistance = targetDistance + plane.stats.searchDistance;
    }

    private void FixedUpdate() {
        searchTarget();
        followTarget();
        checkInScreen();
        transform.Translate(Vector3.right * Time.deltaTime * speed);
    }

    public void searchTarget(){
        var targetResult = SearchTarget.searchTarget(gameObject, plane.stats.searchDistance, targetDistance, transform.right);
        if(targetResult.target != gameObject || target == null){
            target = targetResult.target;
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

    public void checkInScreen(){
        if (transform.position.x < -plane.controller.max  || transform.position.x > plane.controller.max ){
            Destroy(gameObject);
        }

        if (transform.position.z < -plane.controller.maz || transform.position.z > plane.controller.maz ){
            Destroy(gameObject);
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }

    public void OnTriggerEnter(Collision collision)
    {
        Destroy(gameObject);
    }
}
