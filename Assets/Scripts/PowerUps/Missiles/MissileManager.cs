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
    public float maxRotation = 5f;

    public bool explodesMines = false;
    public bool targetAllies = false;

    public bool isExplosion = false;
    public GameObject explosion;
    public bool isCluster = false;
    public int clusterQuantity = 3;
    public GameObject clusterMissiles;
    public bool isFlare = false;
    public GameObject flareArea;

    private void Start() {
        teamManager.team = plane.teamManager.team;
        target = this.gameObject;
        targetDistance = targetDistance + plane.stats.searchDistance;
    }

    private void FixedUpdate() {
        searchTarget();
        followTarget();
        checkInScreen();
        destroyMines();
        transform.Translate(Vector3.right * Time.deltaTime * speed);
    }

    public void searchTarget(){
        var targetResult = SearchTarget.searchTarget(gameObject, plane.stats.searchDistance, targetDistance, transform.right, explodesMines, targetAllies);
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

            if(rotation > maxRotation){
             rotation = maxRotation;
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

    public void OnDestroy()
    {
        if(isExplosion){
            spawnExplosion();
        }
        if(isFlare){
            spawnFlare();
        } 
        if(isCluster){
            spawnCluster();
        }
    }

    public void spawnExplosion(){
        var area = Instantiate(explosion, transform.position, Quaternion.identity);
        area.GetComponent<ExplosionManager>().plane = plane;
    }

    public void spawnFlare(){
        var area = Instantiate(flareArea, transform.position, transform.rotation);
        area.GetComponent<TargetArea>().plane = plane;
    }

    public void spawnCluster(){
        for (int i = 0; i < clusterQuantity; i++){
            var mis = Instantiate(clusterMissiles, transform.position, transform.rotation);
            mis.transform.Rotate(new Vector3(0, 0, (360/clusterQuantity) * (i+1)));
            mis.GetComponent<MissileManager>().plane = plane;
            mis.GetComponent<DamageManager>().damage = plane.stats.missileDamage / clusterQuantity;
        }
    }

    public void destroyMines(){
        try{
            if (explodesMines){
            var mine = target.GetComponent<Mine>();
            if (mine != null && target.GetComponent<TeamManager>().team == plane.teamManager.team && Vector3.Distance(target.transform.position,transform.position) < 0.1){
                mine.spawnBiggerExplosion();
                Destroy(target);
                Destroy(gameObject);
            }
        }
        } catch(Exception e){
            Debug.Log(e);
        }
    }

    
}
