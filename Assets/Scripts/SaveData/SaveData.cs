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
public class SaveData 
{
    public int bestTime = 0;
    public int bestEnemies = 0;
    public int bestTotal= 0;
    public int[] selectedPlayer = {0,0,0,0}; // the plane
    public int[] selectedChar = {0,1,2,3}; //the character
    public int points = 0;
    public bool[] unlockedPlanes;
    public bool[] purchasedPlanes;
    public bool[] unlockedCharacters;
    public bool[] unlockedPowerUps;
    public Upgrades upgrades;
    
    

    public SaveData(PlanesListScriptableObject planeList, CharacterScriptableObjectList characters, int sizeCharacters = 0, int sizePowerUps = 0){
        this.unlockedPlanes = new bool[planeList.planes.Length];
        this.purchasedPlanes = new bool[planeList.planes.Length];
        for (int i=0; i< planeList.planes.Length; i++){
            unlockedPlanes[i] = planeList.planes[i].unlockedByDefault;
            purchasedPlanes[i] = planeList.planes[i].purchasedByDefault;
        }
        this.unlockedCharacters = new bool[sizeCharacters];
        this.unlockedPowerUps = new bool[sizePowerUps];
        upgrades = new Upgrades();
        this.unlockedCharacters = new bool[characters.characters.Length];
        for (int i=0 ; i< unlockedCharacters.Length;i++){
            if (i<6){
                unlockedCharacters[i] = true;
            } else {
                unlockedCharacters[i] = false;
            }
        }
    }
}
