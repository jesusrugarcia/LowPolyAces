using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum gameMode {
    arcade,
    versus,
    race,
    survival
}

[Serializable]
public class GameOptions
{
    public gameMode mode = gameMode.arcade;
    public int playerNum = 1 ;

    public float max = 10;
    public float maz = 5.5f;
    
    //Arcade
    public float timeToIncreaseEnemies = 60;
    public float timeToSpawnPowerUps = 10;
    public int maxPowerUps = 5;

    //Versus
    public float playTime = 180f;
    public int maxLives = 3;

    public GameOptions(){

    }
    
}
