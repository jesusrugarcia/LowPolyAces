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

    const float invAfterImpact = 0.05f;

    public float healing = 0;
    public float healAmount = 0;
    public float healTimer = 0;
    public float burnTimer = 0;

    private void Start() {
    }
    void FixedUpdate()
    {
        checkHealing();
        checkBurning();
    }

    public void checkHealing(){
        if (healing > 0){
            if (plane.stats.health < plane.stats.maxHealth){
                plane.stats.health += healAmount;
            }
            healTimer += Time.deltaTime;
            if (healTimer >= healing){
                healing = 0;
                healTimer = 0;
            }
        }
    }

    public void checkBurning(){
        if(plane.stats.statusEffects[(int)StatusEffects.Burning] > 0){
            burnTimer += Time.deltaTime;
            if(burnTimer >= 1){
                burnTimer = 0;
                damagePlane(null, plane.stats.maxHealth / 50);
            }
        }
    }
    
    private void OnTriggerEnter(Collider other) {
        //Debug.Log("This: " + gameObject.name + " Enemy: " + other.gameObject.name);
        if (other.GetComponent<TeamManager>().team != plane.teamManager.team && plane.stats.statusEffects[(int)StatusEffects.Invulnerability] <= 0){
            damagePlane(other);
            plane.statusManager.addStatus(StatusEffects.Invulnerability , invAfterImpact);
        }
        
    }

    public void damagePlane(Collider other = null, float damage = 0){
            if (damage == 0){
                plane.stats.health += - other.GetComponent<DamageManager>().damage;
            } else {
                plane.stats.health += - damage;
            }
            
            plane.healthBar.setHealth();
            if (plane.stats.health <= 0){
                destroyPlane(other);
            }

            Destroy(other.gameObject);
    }

    public void destroyPlane(Collider other){
        if(plane.controller.gameOptions.mode == gameMode.arcade){
            destroyArcade();
        } else if(plane.controller.gameOptions.mode == gameMode.versus){
            destroyVersus(other);
        }
        //SFXManager.playExplosion();
        
    }

    public void OnDestroy()
    {
         Instantiate(explosion,transform.position, Quaternion.Euler(0,0,0));
         plane.controller.reduceCurrentEnemies();
    }

    public void destroyArcade(){
        if (plane.teamManager.team > 0){ //ojo al gamemodde
            plane.controller.playersAlive += -1; 
            Instantiate(explosion,transform.position, Quaternion.Euler(0,0,0));
            gameObject.SetActive(false);
            plane.healthBar.gameObject.SetActive(false);
        } else {
            Destroy(GetComponent<Rigidbody>());
            plane.controller.score += plane.stats.scoreValue;
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
        Instantiate(explosion,transform.position, Quaternion.Euler(0,0,0));
        gameObject.SetActive(false);
        plane.healthBar.gameObject.SetActive(false);

        manager.spawnPlayer(plane);
    }

}
