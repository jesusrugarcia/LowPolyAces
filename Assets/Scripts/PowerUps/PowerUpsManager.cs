using System;
using System.Collections.Generic;
using UnityEngine;

public enum PowerUps{
    Missile,
    Repair,
    Shield
}

public class PowerUpsManager : MonoBehaviour
{
    public int min = 1;
    public int max = 2;

    public GameObject shield;

    public PowerUps type;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate() {
        randomRotation();
    }

    public void randomRotation(){
        transform.Rotate(UnityEngine.Random.Range(min,max),UnityEngine.Random.Range(min,max),UnityEngine.Random.Range(min,max));
    }

    private void OnCollisionEnter(Collision other) {
        if(type == PowerUps.Missile){
            addMissile(other.gameObject);
        } else if(type == PowerUps.Shield){
            addShield(other.gameObject);
        } else if(type == PowerUps.Repair){
            addHealth(other.gameObject);
        }
    }

    public void addMissile(GameObject plane){
        try {
            var stats = plane.GetComponent<PlaneStats>();
            if(stats.missiles < stats.maxMissiles){
                stats.missiles ++;
                stats.plane.controller.currentPowerUps --;
                Destroy(gameObject);
            }
        } catch(Exception e) {
            Debug.Log(e);
        }
    }

    public void addShield(GameObject plane){
        try {
            var planeManager = plane.GetComponent<PlaneManager>();
            if(!planeManager.stats.hasShield){
                planeManager.stats.hasShield = true;
                planeManager.controller.currentPowerUps --;
                Destroy(gameObject);
                var shi = Instantiate(shield, plane.transform.position,Quaternion.identity);
                var shieldManager = shi.GetComponent<ShieldManager>();
                shieldManager.teamManager.team = planeManager.teamManager.team;
                shieldManager.target = plane; 
            }
        } catch(Exception e){
            Debug.Log(e);
        }
    }

    public void addHealth(GameObject plane){
        try {
            var stats = plane.GetComponent<PlaneStats>();
            if(stats.health < stats.maxHealth){
                stats.health ++;
                stats.plane.controller.currentPowerUps --;
                Destroy(gameObject);
            }
        } catch(Exception e) {
            Debug.Log(e);
        }
    }
}
