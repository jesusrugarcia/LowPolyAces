using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum shootingType{
    normal,
    onButton,
    turret
}

public class PlaneShooter : MonoBehaviour
{
    public GameObject bullet;
    public GameObject missile;
    public float shootTimer = 0;
    public PlaneManager plane;
    public shootingType type;

    public bool magazineFull = false;
    public int magazine;
    public float shootSpeedOnButton = 0.05f;
    
    private void Start() {
        
    }
   
    private void FixedUpdate() {
        checkShoot();
    }

    public virtual void checkShoot(){
        if(type == shootingType.normal){
            shootTimer += Time.deltaTime;
            if (shootTimer >= plane.stats.shootSpeed){
                shootTimer = 0;
                shoot();
            }
        } else if(type == shootingType.onButton){
            if(magazineFull){
                onButtonShoot();
            } else {
                shootTimer += Time.deltaTime;
                if (shootTimer >= plane.stats.shootSpeed){
                    magazineFull = true;
                    magazine = plane.stats.magazineSize;
                    shootTimer = 0;
                }
            }
        }
    }

    public virtual void onButtonShoot(){
        shootTimer += Time.deltaTime;
        if(shootTimer > shootSpeedOnButton){
            shootTimer = 0;
            shoot();
            magazine--;
            if(magazine <= 0){
                magazineFull = false;
            }
        }
    }

    public virtual void shoot(){
        var bull = Instantiate(bullet, transform.position, transform.rotation);
        bull.GetComponent<TeamManager>().team = plane.teamManager.team;
        bull.GetComponent<BulletMovement>().controller = plane.controller;
    }

    public virtual void shoot(GameObject shooter){
        var bull = Instantiate(bullet, shooter.transform.position, shooter.transform.rotation);
        bull.GetComponent<TeamManager>().team = plane.teamManager.team;
        bull.GetComponent<BulletMovement>().controller = plane.controller;
    }

    public void launchMissile(){
        var mis = Instantiate(missile, transform.position + transform.right * 1, transform.rotation);
        mis.GetComponent<MissileManager>().plane = plane;
        plane.stats.missiles --;
    }


}
