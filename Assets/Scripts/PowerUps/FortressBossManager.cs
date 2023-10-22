using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FortressBossManager : BossManager {

    const int DAMAGE = 0;
    const int CLAW = 1;
    const int SHIELD = 2;

     public int behindAngle = 20;
     public bool hasShielded = false;

     public float clawTimer = 0;
     public float maxClawTime = 1;

     public GameObject defenseShield;

    public override void FixedUpdate() {
        base.FixedUpdate();
        checkBehind(behindAngle, true);
        clawTimer += Time.deltaTime;
    }

    public override void activatePhase(){
        phaseActivated = true;
        if (phase == CLAW){
            //activateShield();
            plane.planeShooter.launchMissile();
            clawTimer = 0;
            
        } else if (phase == SHIELD){
            hasShielded = true;
            summonMinions();
            plane.planeShooter.defense = defenseShield;
            plane.planeShooter.Defense();
           
        }
    }

    public override void deactivatePhase(){
        if (phase == CLAW || phase == SHIELD){
            phase = DAMAGE;
            phaseActivated = false;
            
        }  else if (phase == DAMAGE && behindTimer >= maxBehindTime && clawTimer > maxClawTime){
            phase = CLAW;
            behindTimer = 0;
            phaseActivated = false;
        } else if (phase == DAMAGE && !hasShielded && plane.stats.health < plane.stats.maxHealth/4){
            phase = SHIELD;
            behindTimer = 0;
            phaseActivated = false;
        }
    }
}

