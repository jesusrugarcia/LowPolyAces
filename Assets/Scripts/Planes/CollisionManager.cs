using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionManager : MonoBehaviour
{
    //This class manages collisions and damage/destruction of planes.
    public PlaneManager plane;
    public GameObject explosion;
    public SFXManager SFXManager;

    private void Start() {
    }
    
    private void OnTriggerEnter(Collider other) {
        //Debug.Log("This: " + gameObject.name + " Enemy: " + other.gameObject.name);
        if (other.GetComponent<TeamManager>().team != plane.teamManager.team){
            damagePlane(other);
        }
        
    }

    public void damagePlane(Collider other){
        
            plane.stats.health += - other.GetComponent<DamageManager>().damage;
            plane.healthBar.setHealth();
            if (plane.stats.health < 1){
                destroyPlane();
            }

            Destroy(other.gameObject);
    }

    public void destroyPlane(){
        Instantiate(explosion,transform.position, Quaternion.Euler(0,0,0));
        
        if (plane.teamManager.team > 0){ //ojo al gamemodde
            plane.controller.playersAlive += -1; 
            gameObject.SetActive(false);
            plane.healthBar.gameObject.SetActive(false);
        } else {
            plane.controller.score += plane.stats.scoreValue;
            plane.controller.currentEnemies --;
            Destroy(plane.healthBar.gameObject);
            Destroy(gameObject);
        }
        //SFXManager.playExplosion();
        
    }

}