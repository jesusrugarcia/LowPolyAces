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
    public GameObject mine;
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
        instantiateBullet();
        if (plane.stats.extraBullets > 0){
            var offset = 45 / plane.stats.extraBullets;
            for (int i = 0; i < plane.stats.extraBullets; i++){
                instantiateBullet(angle: offset * (i+1));
                instantiateBullet(angle: -offset * (i+1));
            }
        }
    }

    public virtual void instantiateBullet(float angle = 0){
        var bull = Instantiate(bullet, transform.position, transform.rotation);
    //TRACKER BULLET
        if (plane.stats.trackerBullet){
            Destroy(bull.GetComponent<BulletMovement>());
            var manager = bull.AddComponent<MissileManager>();
            manager.plane = plane;
            manager.teamManager = GetComponent<TeamManager>();
            bull.GetComponent<DamageManager>().damage = plane.stats.bulletDamage;
            bull.transform.Rotate(new Vector3(0,0,angle));


        }else {
         //NORMAL BULLET

            bull.GetComponent<TeamManager>().team = plane.teamManager.team;
            var bullMovement = bull.GetComponent<BulletMovement>();
            bullMovement.controller = plane.controller;
            if (bullMovement.isMelee){
                bullMovement.plane = plane;
                bull.GetComponent<DamageManager>().damage = plane.stats.drillDamage;
                bullMovement.meleeOffset = angle * 0.01f;
            } else {
                bull.GetComponent<DamageManager>().damage = plane.stats.bulletDamage;
                bull.transform.Rotate(new Vector3(0,0,angle));
            }
        }
        
    }

    public virtual void shootTurret(GameObject shooter){
        var bull = Instantiate(bullet, shooter.transform.position, shooter.transform.rotation);
        bull.GetComponent<TeamManager>().team = plane.teamManager.team;
        bull.GetComponent<DamageManager>().damage = plane.stats.turretDamage;
        bull.GetComponent<BulletMovement>().controller = plane.controller;
    }

    public void launchMissile(){
        if(plane.stats.specialAmmoType == specialAmmo.Missile){
            var mis = Instantiate(missile, transform.position + transform.right * 1, transform.rotation);
            mis.GetComponent<MissileManager>().plane = plane;
            mis.GetComponent<DamageManager>().damage = plane.stats.missileDamage;
            plane.stats.specialAmmo --;
        } else if(plane.stats.specialAmmoType == specialAmmo.Mine && plane.stats.mines < plane.stats.maxMines){
            var mis = Instantiate(mine, transform.position + transform.right * 1, transform.rotation);
            mis.GetComponent<TeamManager>().team = plane.teamManager.team;
            mis.GetComponent<DamageManager>().damage = plane.stats.mineDamage;
            mis.GetComponent<Mine>().plane = gameObject;
            plane.stats.mines ++;
            plane.stats.specialAmmo --;
        }
        
        
    }


}
