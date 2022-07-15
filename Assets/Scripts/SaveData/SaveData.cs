using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class SaveData 
{
    public int bestTime = 0;
    public int bestEnemies = 0;
    public int bestTotal= 0;
    public int[] selectedPlayer = {0,0,0,0};
    public float points = 0;
    public bool[] unlockedPlanes;

    public SaveData(int bestTime, int bestEnemies, int bestTotal, int size){
        this.bestTime = bestTime;
        this.bestEnemies = bestEnemies;
        this.bestTotal = bestTotal;
        this.unlockedPlanes = new bool[size];
    }
}
