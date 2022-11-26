using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarrierBossManager : BossManager
{
    const int PLANES = 0;
    const int MISSILES = 1;
    const int DAMAGE = 2;

    public float timer = 0;
    public float damageTime = 10;
    public float missileTime = 1;

    public int missilesLaunched = 0;
    
    public GameObject missile;

    public override void Start()
    {
        phase = PLANES;
        plane.stats.maxHealth = plane.controller.gameOptions.playerNum * 200;
        plane.stats.health = plane.controller.gameOptions.playerNum * 200;
    }

     public override void activatePhase(){
        phaseActivated = true;
        if (phase == PLANES){
            //activateShield();
            summonMinions();
            activateShield();
        } else if (phase == MISSILES){
            launchMissiles();
        }
    }

    public override void deactivatePhase(){
        if (phase == PLANES && summonedMinions == 0){
            phase = MISSILES;
            phaseActivated = false;
            missilesLaunched = 0;
            
        } else if(phase == MISSILES){
            launchMissiles();
            if(missilesLaunched >= plane.controller.playersAlive){
                phaseActivated = false;
                phase = DAMAGE;
                timer = 0;
                deactivateShield();
            }
        } else if(phase == DAMAGE){
            timer += Time.deltaTime;
            if(timer >= damageTime){
                timer = 0;
                phaseActivated = false;
                phase = PLANES;
            }
        }
    }

    public void launchMissiles(){
        timer += Time.deltaTime;
        if (timer >= missileTime){
            timer = 0;
            var mis = Instantiate(missile, transform.position + transform.right * 1, transform.rotation);
            mis.GetComponent<MissileManager>().plane = plane;
            mis.GetComponent<DamageManager>().damage = plane.stats.missileDamage;
            missilesLaunched ++;
        }
        
    }

    
}
