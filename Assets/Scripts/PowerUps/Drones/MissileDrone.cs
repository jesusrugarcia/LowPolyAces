using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileDrone : MonoBehaviour
{
    public PlaneManager plane;
    public float timer = 0;

    private void FixedUpdate() {
        timer += Time.deltaTime;
        if(timer >= plane.stats.specialDroneShootSpeed){
            timer = 0;
            var mis =  Instantiate(plane.planeShooter.missile, transform.position, transform.rotation);
            mis.GetComponent<MissileManager>().plane = plane;
            mis.GetComponent<DamageManager>().damage = plane.stats.missileDamage;
        }
    }
}
