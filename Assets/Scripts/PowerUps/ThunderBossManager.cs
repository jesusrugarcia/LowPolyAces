using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunderBossManager : BossManager {
    const int DAMAGE = 0;
    const int DASH = 1;
    const int CLONES = 2;

     public int behindAngle = 140;
     public bool hasCloned = false;

    public override void FixedUpdate() {
        base.FixedUpdate();
        checkBehind(behindAngle);
    }

    public override void activatePhase(){
        phaseActivated = true;
        if (phase == DASH){
            //activateShield();
            plane.planeShooter.Defense();
            
        } else if (phase == CLONES){
            hasCloned = true;
            summonMinions();
            plane.planeShooter.launchGadget();
           
        }
    }

    public override void deactivatePhase(){
        if (phase == DASH || phase == CLONES){
            phase = DAMAGE;
            phaseActivated = false;
            
        }  else if (phase == DAMAGE && behindTimer >= maxBehindTime){
            phase = DASH;
            behindTimer = 0;
            phaseActivated = false;
        } else if (phase == DAMAGE && !hasCloned && plane.stats.health < plane.stats.maxHealth/2){
            phase = CLONES;
            behindTimer = 0;
            phaseActivated = false;
        }
    }

}
