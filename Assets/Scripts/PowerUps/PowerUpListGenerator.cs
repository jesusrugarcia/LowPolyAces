using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PowerUpListGenerator 
{
    public static PowerUpScriptableObject[] selectPowerUps(int powerUpNumber, PowerUpListScriptableObject[] raritiesLists, bool dronesAvailable){
        var powerUps = new PowerUpScriptableObject[powerUpNumber];

        for(int i=0; i<powerUpNumber; i++){
            selectPowerUp(i, powerUps, raritiesLists, dronesAvailable);
        }

        return powerUps;
    }

    public static void selectPowerUp(int i, PowerUpScriptableObject[] powerUps, PowerUpListScriptableObject[] raritiesLists, bool dronesAvailable){
        var rarity = UnityEngine.Random.Range(0,raritiesLists.Length); //Corregir para ajustar rarezas
        powerUps[i] = raritiesLists[rarity].powerUps[UnityEngine.Random.Range(0,raritiesLists[rarity].powerUps.Length)];

        for(int j=0; j<i; j++){
            if( powerUps[j] ==  powerUps[i]
            || ((powerUps[i].type == PowerUps.TurretDrone || powerUps[i].type == PowerUps.RepairDrone || powerUps[i].type == PowerUps.MineDrone || powerUps[i].type == PowerUps.MissileDrone || powerUps[i].type == PowerUps.GunnerDrone || powerUps[i].type == PowerUps.ShieldDrone) && !dronesAvailable)
            ){
                selectPowerUp(i, powerUps, raritiesLists, dronesAvailable);
            }
        }
    }
}
