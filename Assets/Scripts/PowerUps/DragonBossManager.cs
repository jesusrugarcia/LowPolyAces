using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonBossManager : BossManager
{
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

    private void OnDestroy() {
        UnlockManager.unlockPowerUp(plane.controller.data, 2, 0);
    }
}
