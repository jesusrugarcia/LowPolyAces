using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using TMPro;

public class RogueliteModeManager : GameModeManager
{
    public float timer = 0;
    public EnemyList[] enemyLists;

    public TMP_Text scoreText;

    public GameObject powerUpMenu;
    public GameObject ButtonToActivate;

    public GameObject endMenu;
    public GameObject endButton;
    public TMP_Text finalText;

    private bool gameEnded=false;

    public float timeToSpawnEnemies = 3f;
    public bool enemiesSpawned = false;

    public override void StartGame(){
        timer = 0;
        controller.cameraPointCalculator.transform.position = new Vector3(controller.cameraPointCalculator.transform.position.x, 13.5f, controller.cameraPointCalculator.transform.position.z);
        controller.cameraPointCalculator.calculateBoundaries();
        for (int i = 0; i < controller.gameOptions.playerNum; i++){
            controller.players[i] = controller.playerSpawner.spawnPlayer(i,1); //1 for team 1, in arcade mode all players share team.
            controller.playersAlive ++;
        }
        
        scoreText.gameObject.SetActive(false);
        checkPendingPowerUps();
        
    }

    public void checkPendingPowerUps(){ // used to aply power ups bought in shop
        for (int i=0; i < controller.gameOptions.playerNum; i++){
            if (controller.rogueliteSave.pendingPowerUps[i] != null){
                controller.centralManager.managePowerUp(controller.players[i], controller.rogueliteSave.pendingPowerUps[i].type, null);
                controller.rogueliteSave.pendingPowerUps[i] = null;
            }
        }
    }

    public override void UpdateGame(){
        timer += Time.deltaTime;
        if(!enemiesSpawned && timer >= timeToSpawnEnemies){
            spawnEnemies();
            enemiesSpawned = true;
        }
        if (controller.playersAlive > 0 && controller.currentEnemies >0){
            controller.gameTimer += Time.deltaTime;
            controller.totalScore = controller.gameTimer + controller.score;
            scoreText.text = controller.totalScore.ToString("0");
        }
        else if(enemiesSpawned && !gameEnded){
            endGame();
        }
    }

    public void spawnEnemies(){
        for (int i=0; i < controller.rogueliteSave.enemyCount[0] * controller.gameOptions.playerNum; i++){
            var prefab = UnityEngine.Random.Range(0,enemyLists[controller.rogueliteSave.stage].prefabsEasy.planes.Length);
            var enemy = controller.enemySpawner.spawner.spawnPlane(enemyLists[controller.rogueliteSave.stage].prefabsEasy.planes[prefab],null,movement.NoTracking,0);
            enemy.GetComponent<PlaneStats>().speed = enemy.GetComponent<PlaneStats>().maxSpeed;
        }
        //medium
        for (int i=0; i < controller.rogueliteSave.enemyCount[1]* controller.gameOptions.playerNum; i++){
            var prefab = UnityEngine.Random.Range(0,enemyLists[controller.rogueliteSave.stage].prefabsMedium.planes.Length);
            var enemy = controller.enemySpawner.spawner.spawnPlane(enemyLists[controller.rogueliteSave.stage].prefabsMedium.planes[prefab],null,movement.Tracking,0);
            enemy.GetComponent<PlaneStats>().speed = enemy.GetComponent<PlaneStats>().maxSpeed;
        }
        //hard
        for (int i=0; i < controller.rogueliteSave.enemyCount[2]* controller.gameOptions.playerNum; i++){
            var prefab = UnityEngine.Random.Range(0,enemyLists[controller.rogueliteSave.stage].prefabsHard.planes.Length);
            var enemy = controller.enemySpawner.spawner.spawnPlane(enemyLists[controller.rogueliteSave.stage].prefabsHard.planes[prefab],null,movement.Tracking,0);
            enemy.GetComponent<PlaneStats>().speed = enemy.GetComponent<PlaneStats>().maxSpeed;
        }
        //imposible
        for (int i=0; i < controller.rogueliteSave.enemyCount[3]* controller.gameOptions.playerNum; i++){
            var prefab = UnityEngine.Random.Range(0,enemyLists[controller.rogueliteSave.stage].prefabsImposible.planes.Length);
            var enemy = controller.enemySpawner.spawner.spawnPlane(enemyLists[controller.rogueliteSave.stage].prefabsImposible.planes[prefab],null,movement.Tracking,0);
            enemy.GetComponent<PlaneStats>().speed = enemy.GetComponent<PlaneStats>().maxSpeed;
        }

        if(controller.rogueliteSave.boss){
            var enemy = controller.enemySpawner.spawner.spawnPlane(enemyLists[controller.rogueliteSave.stage].prefabsBoss.planes[0],null,movement.Tracking,0);
            enemy.GetComponent<PlaneStats>().speed = enemy.GetComponent<PlaneStats>().maxSpeed;
        }
    }

    public override void endGame()
    {   
        gameEnded = true;
        for (int i=0; i< controller.gameOptions.playerNum;i++){
            controller.rogueliteSave.stats[i].copyStats(controller.players[i].GetComponent<PlaneStats>());
        }
        giveInRunPoints();
        controller.rogueliteSave.loadStats = true;
        FileManager.saveRoguelite(controller.rogueliteSave);
        if(controller.currentEnemies <= 0){
            if(controller.rogueliteSave.boss){ // todo when boss beaten
                endMenu.SetActive(true);
                EventSystem.current.SetSelectedGameObject(null);
                EventSystem.current.SetSelectedGameObject(endButton);
                EventSystem.current.SetSelectedGameObject(endButton, new BaseEventData(EventSystem.current));
                givePoints(150);
            } else {
                var menuManager = powerUpMenu.GetComponent<PowerUpMenuManager>();
                menuManager.selectPowerUps();
                powerUpMenu.SetActive(true);
                
                EventSystem.current.SetSelectedGameObject(null);
                EventSystem.current.SetSelectedGameObject(ButtonToActivate);
                EventSystem.current.SetSelectedGameObject(ButtonToActivate, new BaseEventData(EventSystem.current));
            }
            
            
            //Time.timeScale = 0.0f;
        } else {
            endMenu.SetActive(true);
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(endButton);
            EventSystem.current.SetSelectedGameObject(endButton, new BaseEventData(EventSystem.current));

            givePoints(50);
        }
        
    }

    public void givePoints(float multiplier){
        var map = FileManager.loadMap();
        controller.data.points += (int)(map.nodes[map.currentMapNode].layer * multiplier);
        controller.rogueliteSave.loadMap = false;
        controller.rogueliteSave.money = new int[] {0,0,0,0};
        finalText.text = "Gained points: " +  (map.nodes[map.currentMapNode].layer * multiplier).ToString();
        FileManager.saveRoguelite(controller.rogueliteSave);
        FileManager.saveData(controller.data);
    }

    public void giveInRunPoints(){
        var map = FileManager.loadMap();
        var points = map.nodes[map.currentMapNode].layer + map.nodes[map.currentMapNode].totalEnemies() + Random.Range(10,16);

        for(int i=0; i< controller.rogueliteSave.money.Length; i++){
            controller.rogueliteSave.money[i] += points;
        }
        FileManager.saveRoguelite(controller.rogueliteSave);
    }

    public void backToMenu(){
        
        SceneManager.LoadScene(0);
    }
}
