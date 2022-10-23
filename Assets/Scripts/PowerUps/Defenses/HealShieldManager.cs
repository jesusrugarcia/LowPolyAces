using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealShieldManager : MonoBehaviour
{
    public PlaneManager plane;
    public float time = 0;
    // Start is called before the first frame update
   

    private void FixedUpdate() {
        transform.position = plane.transform.position;
        time += Time.deltaTime;
        if ( time >= plane.stats.SpecialShieldDuration){
            destroyShield();
        }
    }

    private void OnTriggerEnter(Collider other) {
        //Debug.Log("This: " + gameObject.name + " Enemy: " + other.gameObject.name);
        if (other.GetComponent<TeamManager>().team != plane.teamManager.team){
            Destroy(other.gameObject);
            HealPlane(other);
        }
        
    }

    public void HealPlane(Collider other){
        if (plane.stats.health < plane.stats.maxHealth){
            plane.stats.health += other.gameObject.GetComponent<DamageManager>().damage;
            if (plane.stats.health > plane.stats.maxHealth){
            plane.stats.health = plane.stats.maxHealth;
            } 
        } 
    }

    public void destroyShield(){
        plane.planeShooter.defenseActivated = false;
        plane.stats.defenseAmmo --;
        Destroy(gameObject);
    }
}
