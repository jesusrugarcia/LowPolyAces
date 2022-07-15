using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public float gameTimer = 0;
    public float score = 0;
    public float totalScore = 0;

    public float max = 10;
    public float maz = 5.5f;

    public int playersAlive = 0;
    public GameObject[] players;

    public GameObject onFinish;
    public Text scoreText;
    public Text timeText;
    public Text enemiesText;
    public Text totalText;
    public Text previousBestText;
    public Text gainedPoints;

    public SaveData data;
    public GameOptions gameOptions;
    public FileManager saveManager = new FileManager();

    [SerializeField]
    public GameModeManager gameModeManager;
    public EnemySpawner enemySpawner;
    public PlayerSpawner playerSpawner;
    public PowerUpSpawner powerUpSpawner;

    public int currentEnemies = 0;
    public int currentPowerUps = 0;
    //public ObjectPools pools;

    private void Start() {
        data = saveManager.loadData(4); //ojo que esto es el numero de aviones en el juego.
        gameOptions = saveManager.loadOptions();

        max = gameOptions.max;
        maz = gameOptions.maz;
        players = new GameObject[gameOptions.playerNum];
        Time.timeScale = 1f;

        loadModeManager();
        gameModeManager.StartGame();
    }

    private void FixedUpdate() {
        gameModeManager.UpdateGame();
    }

    public void loadModeManager(){
        if (gameOptions.mode == gameMode.arcade){
            gameModeManager = new ArcadeModeManager();
        } else {//placeholder
            gameModeManager = new ArcadeModeManager();
        }

        gameModeManager.controller = this;
    }

    
}
