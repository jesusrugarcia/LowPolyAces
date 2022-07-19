using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VersusModeManager : GameModeManager
{
    public float timer = 0;
    public float[] score;
    public int[] lives;

    public override void StartGame()
    {
        score = new float[] {0,0,0,0};
        lives = new int[] {0,0,0,0};
    }

    public override void UpdateGame()
    {
        timer += Time.deltaTime;
        if(timer >= controller.gameOptions.playTime){
            endGame();
        }
    }

    public override void endGame(){

    }
}
