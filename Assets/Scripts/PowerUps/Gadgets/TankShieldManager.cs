using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankShieldManager : MonoBehaviour
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
            damagePlane(other);
        }
        
    }

    public void damagePlane(Collider other){
        plane.collissionManager.damagePlane(other, other.GetComponent<DamageManager>().damage * plane.stats.damageReductionTankShield);
    }

    public void destroyShield(){
        plane.planeShooter.defenseActivated = false;
        Destroy(gameObject);
    }
}
