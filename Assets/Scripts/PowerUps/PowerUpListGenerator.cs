using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PowerUpListGenerator 
//this class manages the generation of selectable power up list after beating a level
{
    public static PowerUpScriptableObject[] selectPowerUps(int powerUpNumber, PowerUpListScriptableObject[] powerLists, bool dronesAvailable, RogueliteSave save){
        var powerUps = new PowerUpScriptableObject[powerUpNumber];
        var rarities = new int[4] {70, 85, 95, 100};
        for(int i=0; i<powerUpNumber; i++){
            selectPowerUp(i, powerUps, powerLists, rarities, dronesAvailable, save);
        }

        return powerUps;
    }

    public static void selectPowerUp(int i, PowerUpScriptableObject[] powerUps, PowerUpListScriptableObject[] powerLists, int[]rarities, bool dronesAvailable, RogueliteSave save){
        var rarity = UnityEngine.Random.Range(0,rarities[rarities.Length -1]) + (save.stage * 2);
        Debug.Log("Rarity for powerUp" + i + " is: " + rarity);
        if (rarity <= rarities[0]){
             powerUps[i] = powerLists[0].powerUps[UnityEngine.Random.Range(0,powerLists[0].powerUps.Length)];
        } else if (rarity <= rarities[1]){
             powerUps[i] = powerLists[1].powerUps[UnityEngine.Random.Range(0,powerLists[1].powerUps.Length)];
        } else if (rarity <= rarities[2]){
             powerUps[i] = powerLists[2].powerUps[UnityEngine.Random.Range(0,powerLists[2].powerUps.Length)];
        } else {
            powerUps[i] = powerLists[3].powerUps[UnityEngine.Random.Range(0,powerLists[3].powerUps.Length)];
        }

        

        for(int j=0; j<i; j++){
            if( powerUps[j] ==  powerUps[i] //check if power up is valid
            || ((powerUps[i].type == PowerUps.TurretDrone || powerUps[i].type == PowerUps.RepairDrone || powerUps[i].type == PowerUps.MineDrone || powerUps[i].type == PowerUps.MissileDrone || powerUps[i].type == PowerUps.GunnerDrone || powerUps[i].type == PowerUps.ShieldDrone) && !dronesAvailable)
            ){
                selectPowerUp(i, powerUps, powerLists, rarities, dronesAvailable,save);
            }
        }
    }
}
