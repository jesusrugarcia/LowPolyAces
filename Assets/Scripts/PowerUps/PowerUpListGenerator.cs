using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PowerUpListGenerator 
//this class manages the generation of selectable power up list after beating a level
{
    public static PowerUpScriptableObject[] selectPowerUps(int powerUpNumber, PowerUpListScriptableObject[] powerLists, bool dronesAvailable, RogueliteSave save, SaveData data, int minRarity = 0){
        var powerUps = new PowerUpScriptableObject[powerUpNumber];
        var rarities = new int[4] {70, 85, 95, 100};
        for(int i=0; i<powerUpNumber; i++){
            selectPowerUp(i, powerUps, powerLists, rarities, dronesAvailable, save, data, minRarity);
        }

        return powerUps;
    }

    public static void selectPowerUp(int i, PowerUpScriptableObject[] powerUps, PowerUpListScriptableObject[] powerLists, int[]rarities, bool dronesAvailable, RogueliteSave save, SaveData data,int minRarity = 0){
        var rarity = UnityEngine.Random.Range(minRarity,rarities[rarities.Length -1]);
        var index = 0;
        var rar = 0;
        
        if (rarity <= rarities[0]){
            index = UnityEngine.Random.Range(0,powerLists[0].powerUps.Length);
             powerUps[i] = powerLists[0].powerUps[index];
             rar = 0;
        } else if (rarity <= rarities[1]){
            index = UnityEngine.Random.Range(0,powerLists[1].powerUps.Length);
             powerUps[i] = powerLists[1].powerUps[index];
             rar = 1;
        } else if (rarity <= rarities[2]){
            index = UnityEngine.Random.Range(0,powerLists[2].powerUps.Length);
             powerUps[i] = powerLists[2].powerUps[index];
             rar = 2;
            index = UnityEngine.Random.Range(0,powerLists[3].powerUps.Length);
            powerUps[i] = powerLists[3].powerUps[index];
            rar = 3;
        }

        Debug.Log("Rarity for powerUp" + i + " is: " + rarity + " index is " + index);

        

        for(int j=0; j<i; j++){
            try{
                if( powerUps[j] ==  powerUps[i] //check if power up is valid
                || ((powerUps[i].type == PowerUps.TurretDrone || powerUps[i].type == PowerUps.RepairDrone || powerUps[i].type == PowerUps.MineDrone || powerUps[i].type == PowerUps.MissileDrone || powerUps[i].type == PowerUps.GunnerDrone || powerUps[i].type == PowerUps.ShieldDrone) && !dronesAvailable)
                || data.unlockedPowerUps[rar].powerUps[index] == false ){
                    selectPowerUp(i, powerUps, powerLists, rarities, dronesAvailable,save,data);
                }
            } catch( System.Exception e){
                Debug.Log(e);
                selectPowerUp(i, powerUps, powerLists, rarities, dronesAvailable,save,data);
            }
            
        }
    }
}
