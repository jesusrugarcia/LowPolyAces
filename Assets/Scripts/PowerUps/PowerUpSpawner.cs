using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpawner : MonoBehaviour
{
    public GameController controller;
    public GameObject missile;
    public GameObject mine;
    public GameObject shield;
    public GameObject repair;
    public GameObject turretDrone;
    public GameObject MineDrone;
    public GameObject gunnerDrone;
    public GameObject MissileDrone;
    public GameObject RepairDrone;
    public GameObject ShieldDrone;
    public GameObject MissileBullet;
    public GameObject DrillBullet;
    public GameObject ExtraBullet;
    public GameObject TrackerBullet;

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
        } else if (type == PowerUps.TurretDrone){
            Instantiate(turretDrone,getPosition(),Quaternion.identity);
        } else if (type == PowerUps.Mine){
            Instantiate(mine,getPosition(),Quaternion.identity);
        } else if (type == PowerUps.MineDrone){
            Instantiate(MineDrone,getPosition(),Quaternion.identity);
        } else if (type == PowerUps.GunnerDrone){
            Instantiate(gunnerDrone,getPosition(),Quaternion.identity);
        }else if (type == PowerUps.MissileDrone){
            Instantiate(MissileDrone,getPosition(),Quaternion.identity);
        }else if (type == PowerUps.RepairDrone){
            Instantiate(RepairDrone,getPosition(),Quaternion.identity);
        }else if (type == PowerUps.ShieldDrone){
            Instantiate(ShieldDrone,getPosition(),Quaternion.identity);
        }else if (type == PowerUps.MissileBullet){
            Instantiate(MissileBullet,getPosition(),Quaternion.identity);
        }else if (type == PowerUps.DrillBullet){
            Instantiate(DrillBullet,getPosition(),Quaternion.identity);
        }else if (type == PowerUps.ExtraBullet){
            Instantiate(ExtraBullet,getPosition(),Quaternion.identity);
        }else if (type == PowerUps.TrackerBullet){
            Instantiate(TrackerBullet,getPosition(),Quaternion.identity);
        }
        controller.currentPowerUps ++;
    }

    public Vector3 getPosition(){
        var x = UnityEngine.Random.Range(-controller.max, controller.max);
        var z = UnityEngine.Random.Range(-controller.maz, controller.maz);

        return new Vector3(x,0,z);
    }
}
