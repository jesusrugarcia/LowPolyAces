using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Upgrades{
    public int[] increase = {0};
    public int[] costPerIncrease = {0};
    public int[] max = {0};
}

[Serializable]
public class unlockedPowerUps{
    public bool[] powerUps;
}

[Serializable]
public class SaveData 
{
    public int bestTime = 0;
    public int bestEnemies = 0;
    public int bestTotal= 0;
    public int[] selectedPlayer = {0,0,0,0}; // the plane
    public int[] selectedChar = {0,1,2,3}; //the character
    public int points = 0;
    public bool[] unlockedPlanes;
    public bool[] notifyPlanes;
    public bool[] purchasedPlanes;
    public bool[] unlockedCharacters;
    public bool[] notifyCharacters;
    public unlockedPowerUps[] unlockedPowerUps;
    public unlockedPowerUps[] notifyPowerUps;
    public Upgrades upgrades;
    public bool notify = false;
    
    

    public SaveData(PlanesListScriptableObject planeList, CharacterScriptableObjectList characters, PowerUpListScriptableObject[] powerUps){
        unlockedPlanes = new bool[planeList.planes.Length];
        notifyPlanes = new bool[planeList.planes.Length];
        purchasedPlanes = new bool[planeList.planes.Length];
        for (int i=0; i< planeList.planes.Length; i++){
            unlockedPlanes[i] = planeList.planes[i].unlockedByDefault;
            purchasedPlanes[i] = planeList.planes[i].purchasedByDefault;
        }
        
        upgrades = new Upgrades();
        unlockedCharacters = new bool[characters.characters.Length];
        notifyCharacters = new bool[characters.characters.Length];
        for (int i=0 ; i< unlockedCharacters.Length;i++){
                unlockedCharacters[i] = characters.characters[i].initialUnlock;
            
        }

        unlockedPowerUps = new unlockedPowerUps[powerUps.Length];
        notifyPowerUps = new unlockedPowerUps[powerUps.Length];
        for (int i=0 ; i< unlockedPowerUps.Length; i++){
            unlockedPowerUps[i] = new unlockedPowerUps();
            notifyPowerUps[i] = new unlockedPowerUps();
            unlockedPowerUps[i].powerUps = new bool[powerUps[i].powerUps.Length];
            notifyPowerUps[i].powerUps = new bool[powerUps[i].powerUps.Length];
            for (int y=0; y< unlockedPowerUps[i].powerUps.Length; y++){
                unlockedPowerUps[i].powerUps[y] = powerUps[i].powerUps[y];
            }
        }
    }
}
