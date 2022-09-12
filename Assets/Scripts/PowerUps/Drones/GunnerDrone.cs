using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunnerDrone : MonoBehaviour
{
    public PlaneManager plane;
    public float timer = 0;

    private void FixedUpdate() {
        timer += Time.deltaTime;
        if(timer >= plane.stats.normalDroneShootSpeed){
            timer = 0;
            var bullet =  Instantiate(plane.planeShooter.bullet, transform.position, transform.rotation);
            bullet.GetComponent<TeamManager>().team = plane.teamManager.team;
            bullet.GetComponent<BulletMovement>().controller = plane.controller;
            bullet .GetComponent<DamageManager>().damage = plane.stats.bulletDamage; // may have to change;
        }
    }
}
