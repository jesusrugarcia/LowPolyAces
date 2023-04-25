using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public CameraPointCalculator cameraPointCalculator;
    public AudioManager audioManager;
    public GameMenu gameMenu;
    public GameObject ButtonToActivate;
    public GameObject EvadedText;

    public float gameTimer = 0;
    public float score = 0;
    public float totalScore = 0;

    public float max = 10;
    public float maz = 5.5f;

    public int playersAlive = 0;
    public GameObject[] players;


    public SaveData data;
    public GameOptions gameOptions;
    public RogueliteSave rogueliteSave;
    [SerializeField]
    public CharacterScriptableObjectList characters;
    

    [SerializeField]
    public GameModeManager gameModeManager;
    public EnemySpawner enemySpawner;
    public PlayerSpawner playerSpawner;
    public PowerUpSpawner powerUpSpawner;
    public PowerUpsCentralManager centralManager;

    public int currentEnemies = 0;
    public int maxEnemies = 1;
    public int currentPowerUps = 0;
    //public ObjectPools pools;

    public MapGenerator mapGenerator;

    private void Start() {
        rogueliteSave = FileManager.loadRoguelite();
        data = FileManager.loadData(playerSpawner.playerList, characters); //ojo que esto es el numero de aviones en el juego.
        gameOptions = FileManager.loadOptions();
        cameraPointCalculator.calculateBoundaries();
        audioManager.updateVolume();
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
            gameModeManager = GetComponent<ArcadeModeManager>();
        } else  if (gameOptions.mode == gameMode.versus){//placeholder
            gameModeManager = GetComponent<VersusModeManager>();
        } else  if (gameOptions.mode == gameMode.roguelite){//placeholder
            gameModeManager = GetComponent<RogueliteModeManager>();
            generateMap();
        }

        gameModeManager.controller = this;
    }

    public void reduceCurrentEnemies(){
        currentEnemies --;
        if (currentEnemies < 0){
            currentEnemies = 0;
        }
    }

    public void reduceCurrentPowerUps(){
        currentPowerUps --;
        if (currentPowerUps < 0){
            currentPowerUps = 0;
        }
    }

    public void generateMap(){
        Debug.Log(rogueliteSave.seed);
        mapGenerator.seed= rogueliteSave.seed;
        mapGenerator.GenerateMap();
    }

    public void setVolume(){
        audioManager.updateVolume();
    }

    
}
