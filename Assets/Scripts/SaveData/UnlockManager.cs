using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this class manages all the unlocks in the game;
public static class UnlockManager 
{
    public static void unlockCharacter(SaveData data, int pos){
        if (data.unlockedCharacters[pos] == false){
            data.unlockedCharacters[pos] = true;
            data.notifyCharacters[pos] = true;
            data.notify = true;
            FileManager.saveData(data);
        }
        
    }

    public static void unlockPlane(SaveData data, int pos){
        if(!data.unlockedPlanes[pos]){
            data.unlockedPlanes[pos] = true;
            data.notifyPlanes[pos] = true;
            data.notify = true;
            FileManager.saveData(data);
        }
        
    }

    public static void unlockPowerUp(SaveData data, int list, int pos){
        if (!data.unlockedPowerUps[list].powerUps[pos]){
            data.unlockedPowerUps[list].powerUps[pos] = true;
            data.notifyPowerUps[list].powerUps[pos] = true;
            data.notify = true;
            FileManager.saveData(data);
        }
        
    }
}
