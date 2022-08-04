using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VersusModeManager : GameModeManager
{
    public float timer = 0;
    public float[] score;
    public int[] lives;

    public GameObject onFinish;
    public Text player1;
    public Text player2;
    public Text player3;
    public Text player4;
    public Text winner;
    public Text time;


    public override void StartGame()
    {
        score = new float[] {0,0,0,0};
        lives = new int[] {0,0,0,0};

        for (int i = 0; i < controller.gameOptions.playerNum; i++){
            controller.players[i] = controller.playerSpawner.spawnPlayer(i,i); //1 for team 1, in arcade mode all players share team.
            controller.playersAlive ++;
        }
    }

    public override void UpdateGame()
    {
        timer += Time.deltaTime;
        if(timer >= controller.gameOptions.playTime){
            endGame();
        }
        time.text = (controller.gameOptions.playTime - timer).ToString("0");
    }

    public void checkWinner(){
        for(int i = 0; i < score.Length; i++){
            if(lives[i] >= controller.gameOptions.maxLives){
                endGame();
            }
        }
    }

    public override void endGame(){
        Time.timeScale = 0.0f;
        controller.playersAlive = 0;
        onFinish.SetActive(true);
        player1.text = "Player 1 score:" + score[0];
        player2.text = "Player 2 score:" + score[1];
        player3.text = "Player 3 score:" + score[2];
        player4.text = "Player 4 score:" + score[3];

        getWinner();

        controller.data.points += (score[0] + score[1] + score[2] + score[3]);
        controller.saveManager.saveData(controller.data);
    }

    public void getWinner(){
        var max = 0f;
        var win = 0;

        for(int i = 0; i < score.Length; i++){
            if(score[i] > max){
                max = score[i];
                win = i;
            }
        }

        winner.text= "Player "+ (win+1) + " Wins!";
    }

    public void spawnPlayer(PlaneManager plane){

        plane.gameObject.transform.position = controller.playerSpawner.spawner.getRandomPosition();
        controller.playerSpawner.spawner.calculateRotation(plane.gameObject);

        plane.stats.health = plane.stats.maxHealth;
        plane.stats.missiles = 0;

        plane.gameObject.SetActive(true);
        plane.healthBar.gameObject.SetActive(true);
        controller.playersAlive++;
        
    }
}
