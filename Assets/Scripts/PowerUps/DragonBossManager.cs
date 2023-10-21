using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonBossManager : BossManager
{
    const int FIRE = 0;
    const int DRONES = 1;

    public float dashSpeed = 15;
    public int behindAngle = 100;

    public int dashCount = 0;
    public int maxDashes = 3;
    public float dashTimer = 0;
    public float maxDashTime = 1;

    public GameObject bodyObject;
    public GameObject tailObject;
    public int bodyCount;
    public GameObject lastPart;

    public float partDistance = 0.85f;
    public float partRotation = 1.5f;
    public override void Start() {
        base.Start();
        lastPart = gameObject;
        for (int i=0; i< bodyCount; i++){
            spawnBodyPart(bodyObject);
        }
        spawnBodyPart(tailObject);
    }

    public void spawnBodyPart(GameObject part){
        var newPart = Instantiate(part,this.transform.position, this.transform.rotation);
        var manager = newPart.GetComponent<DragonBodyManager>();
        manager.manager = this;
        manager.target = lastPart;
        lastPart = newPart;
    }

    public override void FixedUpdate() {
        base.FixedUpdate();
        checkBehind(behindAngle);
    }

    public override void activatePhase(){
        phaseActivated = true;
        if (phase == DRONES){
            //activateShield();
            plane.planeShooter.launchGadget();
            dashCount += 1;
            summonMinions();
        } else if (phase == FIRE){
            
        }
    }

    public override void deactivatePhase(){
        if (phase == DRONES){
            if (dashCount >= maxDashes){
                phase = FIRE;
                phaseActivated = false;
            } else if (dashTimer >= maxDashTime){
                dashTimer = 0;
                plane.planeShooter.launchGadget();
                dashCount += 1;
            } else {
                dashTimer += Time.deltaTime;
            }
            
        } else if (phase == FIRE && behindTimer >= maxBehindTime){
            phase = DRONES;
            behindTimer = 0;
            phaseActivated = false;
        }
    }

    private void OnDestroy() {
        
    }
}
