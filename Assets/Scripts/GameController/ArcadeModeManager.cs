using UnityEngine.EventSystems;
using UnityEngine;

using TMPro;

public class ArcadeModeManager : GameModeManager
{
    
    public float timeToIncreaseEnemies = 20;
    public float timer = 0f;
    public int powerUpsSize = 0;
    public float timerBoss = 0;
    public float timerForBoss = 300;

    public GameObject onFinish;
    public GameObject ButtonToActivate;
    public TMP_Text scoreText;
    public TMP_Text timeText;
    public TMP_Text enemiesText;
    public TMP_Text totalText;
    public TMP_Text previousBestText;
    public TMP_Text gainedPoints;

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
            scoreText.text = controller.totalScore.ToString("0");

            powerUpCheck();
        }
        else {
            endGame();
        }
        controller.maxEnemies = (int)((controller.gameTimer/timeToIncreaseEnemies) + controller.gameOptions.playerNum);

        spawnEnemies();
    }

    public void spawnEnemies(){
        if (controller.currentEnemies < controller.maxEnemies){
            controller.enemySpawner.spawnEnemy();
        }
        timerBoss += Time.deltaTime;
        if (timerBoss >= timerForBoss){
            timerBoss = 0;
            var enemy = controller.enemySpawner.spawner.spawnPlane(controller.enemySpawner.enemyList.prefabsBoss.planes[0],null,movement.Tracking,0);
            enemy.GetComponent<PlaneStats>().speed = enemy.GetComponent<PlaneStats>().maxSpeed;
        }
    }

    public override void endGame(){
        
        controller.playersAlive = 0;
        onFinish.SetActive(true);
        
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(ButtonToActivate);
        EventSystem.current.SetSelectedGameObject(ButtonToActivate, new BaseEventData(EventSystem.current));
        
        Time.timeScale = 0.0f;
        previousBestText.text = "Previous best score: " + controller.data.bestTotal.ToString("0");
        if(controller.data.bestTime < (int)controller.gameTimer){
            timeText.text = "Time survived: " + controller.gameTimer.ToString("0") + " New Record!";
            controller.data.bestTime = (int)controller.gameTimer;
        } else {
            timeText.text = "Time survived: " + controller.gameTimer.ToString("0");
        }

        if(controller.data.bestEnemies < (int)controller.score){
            enemiesText.text = "Enemies destroyed: " + controller.score.ToString("0") + " New Record!";
            controller.data.bestEnemies = (int)controller.score;
        } else {
            enemiesText.text = "Enemies destroyed: " + controller.score.ToString("0");
        }

        if(controller.data.bestTotal < (int)(controller.gameTimer + controller.score)){
            totalText.text = "Total score: " + (controller.gameTimer + controller.score).ToString("0") + " New Record!";
            controller.data.bestTotal = (int)(controller.gameTimer + controller.score);
        } else {
            totalText.text = "Total score: " + (controller.gameTimer + controller.score).ToString("0");
        }
        controller.data.points += (int)((controller.gameTimer + controller.score)/10);
        gainedPoints.text = "Gained Points: " + ((controller.gameTimer + controller.score)/10).ToString("0");
        FileManager.saveData(controller.data);
        

        scoreText.gameObject.SetActive(false);

    }

}
