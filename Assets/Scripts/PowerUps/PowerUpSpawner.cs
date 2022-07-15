using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpawner : MonoBehaviour
{
    public GameController controller;
    public GameObject missile;
    public GameObject shield;
    public GameObject repair;

    public void spawnPowerUp(PowerUps type){ //to be updated with propper elements...
        if(controller.currentPowerUps >= controller.gameOptions.maxPowerUps){
            return;
        }

        if(type == PowerUps.Missile){
            Instantiate(missile,getPosition(),Quaternion.identity);
        } else if(type == PowerUps.Repair){
            Instantiate(repair,getPosition(),Quaternion.identity);
        } else if(type == PowerUps.Shield){
            Instantiate(shield,getPosition(),Quaternion.identity);
        }
        controller.currentPowerUps ++;
    }

    public Vector3 getPosition(){
        var x = UnityEngine.Random.Range(-controller.max, controller.max);
        var z = UnityEngine.Random.Range(-controller.maz, controller.maz);

        return new Vector3(x,0,z);
    }
}
