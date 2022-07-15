using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcadeModeManager : GameModeManager
{
    public int maxEnemies = 1;
    public float timeToIncreaseEnemies = 20;

    
    public float timer = 0f;

    public int powerUpsSize = 0;


    public void powerUpCheck() {
        timer += Time.deltaTime;
        if(timer >= controller.gameOptions.timeToSpawnPowerUps){
            spawnPowerUp();
            timer = 0;
        }
    }

    public void spawnPowerUp(){
        var type = UnityEngine.Random.Range(0, powerUpsSize);
        controller.powerUpSpawner.spawnPowerUp((PowerUps)type);
    }

    
    public override void StartGame(){
        timeToIncreaseEnemies = controller.gameOptions.timeToIncreaseEnemies;
        powerUpsSize = PowerUps.GetNames(typeof(PowerUps)).Length;

        for (int i = 0; i < controller.gameOptions.playerNum; i++){
            controller.players[i] = controller.playerSpawner.spawnPlayer(i,1); //1 for team 1, in arcade mode all players share team.
            controller.playersAlive ++;
        }
    }

    public override void UpdateGame(){
        if (controller.playersAlive > 0){
            controller.gameTimer += Time.deltaTime;
            controller.totalScore = controller.gameTimer + controller.score;
            controller.scoreText.text = controller.totalScore.ToString("0");

            powerUpCheck();
        }
        else {
            endGame();
        }
        maxEnemies = (int)((controller.gameTimer/timeToIncreaseEnemies) + 1);

        spawnEnemies();
    }

    public void spawnEnemies(){
        if (controller.currentEnemies < maxEnemies){
            controller.enemySpawner.spawnEnemy();
        }
    }

    public void endGame(){
        Time.timeScale = 0.0f;
        controller.playersAlive = 0;
        controller.onFinish.SetActive(true);

        controller.previousBestText.text = "Previous best score: " + controller.data.bestTotal.ToString("0");
        if(controller.data.bestTime < (int)controller.gameTimer){
            controller.timeText.text = "Time survived: " + controller.gameTimer.ToString("0") + " New Record!";
            controller.data.bestTime = (int)controller.gameTimer;
        } else {
            controller.timeText.text = "Time survived: " + controller.gameTimer.ToString("0");
        }

        if(controller.data.bestEnemies < (int)controller.score){
            controller.enemiesText.text = "Enemies destroyed: " + controller.score.ToString("0") + " New Record!";
            controller.data.bestEnemies = (int)controller.score;
        } else {
            controller.enemiesText.text = "Enemies destroyed: " + controller.score.ToString("0");
        }

        if(controller.data.bestTotal < (int)(controller.gameTimer + controller.score)){
            controller.totalText.text = "Total score: " + (controller.gameTimer + controller.score).ToString("0") + " New Record!";
            controller.data.bestTotal = (int)(controller.gameTimer + controller.score);
        } else {
            controller.totalText.text = "Total score: " + (controller.gameTimer + controller.score).ToString("0");
        }
        controller.data.points += (controller.gameTimer + controller.score)/10;
        controller.gainedPoints.text = "Gained Points: " + ((controller.gameTimer + controller.score)/10).ToString("0");
        controller.saveManager.saveData(controller.data);
        

        controller.scoreText.gameObject.SetActive(false);

    }
}
