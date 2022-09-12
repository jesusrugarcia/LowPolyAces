using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionManager : MonoBehaviour
{
    //This class manages collisions and damage/destruction of planes.
    public PlaneManager plane;
    public GameObject explosion;
    public GameObject shield;
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
            if (plane.stats.health <= 0){
                destroyPlane(other);
            }

            Destroy(other.gameObject);
    }

    public void destroyPlane(Collider other){
        Instantiate(explosion,transform.position, Quaternion.Euler(0,0,0));
        
        if(plane.controller.gameOptions.mode == gameMode.arcade){
            destroyArcade();
        } else if(plane.controller.gameOptions.mode == gameMode.versus){
            destroyVersus(other);
        }
        //SFXManager.playExplosion();
        
    }

    public void destroyArcade(){
        if (plane.teamManager.team > 0){ //ojo al gamemodde
            plane.controller.playersAlive += -1; 
            gameObject.SetActive(false);
            plane.healthBar.gameObject.SetActive(false);
        } else {
            Destroy(GetComponent<Rigidbody>());
            plane.controller.score += plane.stats.scoreValue;
            plane.controller.reduceCurrentEnemies();
            Destroy(plane.healthBar.gameObject);
            Destroy(gameObject);
        }
    }

    public void destroyVersus(Collider other){
        //Destroy(GetComponent<Rigidbody>());
        var manager =  plane.controller.GetComponent<VersusModeManager>();
        manager.lives[plane.teamManager.team]++;
        manager.score[other.gameObject.GetComponent<TeamManager>().team]++;
        manager.checkWinner();

        gameObject.SetActive(false);
        plane.healthBar.gameObject.SetActive(false);

        manager.spawnPlayer(plane);
    }

}
