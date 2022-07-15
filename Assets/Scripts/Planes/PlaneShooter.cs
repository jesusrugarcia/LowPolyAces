using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneShooter : MonoBehaviour
{
    public GameObject bullet;
    public GameObject missile;
    public float shootTimer = 0;
    public PlaneManager plane;
    
    private void Start() {
        
    }
   
    private void FixedUpdate() {
        shootTimer += Time.deltaTime;
        if (shootTimer >= plane.stats.shootSpeed){
            shootTimer = 0;
            shoot();
        }
    }

    public void shoot(){
        var bull = Instantiate(bullet, transform.position, transform.rotation);
        bull.GetComponent<TeamManager>().team = plane.teamManager.team;
        bull.GetComponent<BulletMovement>().controller = plane.controller;
    }

    public void launchMissile(){
        var mis = Instantiate(missile, transform.position + transform.right * 1, transform.rotation);
        mis.GetComponent<MissileManager>().plane = plane;
        plane.stats.missiles --;
    }


}
