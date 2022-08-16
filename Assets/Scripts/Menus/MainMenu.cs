using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject optionsMenu;
    public GameObject optionsButton;
    public GameObject menu;
    public GameObject menuButton;
    public GameObject playMenu;
    public GameObject playButton;

    public GameOptions gameOptions;

    public Text playersText;
    public Text timeToSpawnPowerUpsText;
    public Text maxPowerUpsText;
    public Text arcadeSpawnTimeText;
    public Text versusTimeText;
    public Text versusLifesText;

    void Start()
    {
         gameOptions = FileManager.loadOptions();
    }

    public void playGame(){
        playMenu.SetActive(true);
        menu.SetActive(false);

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(playButton);
        EventSystem.current.SetSelectedGameObject(playButton, new BaseEventData(EventSystem.current));
        
    }

    public void closePlayMenu(){
        playMenu.SetActive(false);
        menu.SetActive(true);

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(menuButton);
        EventSystem.current.SetSelectedGameObject(menuButton, new BaseEventData(EventSystem.current));
    }

    public void play(){
        FileManager.saveOptions(gameOptions);
        SceneManager.LoadScene(1);
    }

    public void quitGame(){
        Application.Quit();
    }

    public void mainMenu(){
        SceneManager.LoadScene(0);
    }

    public void shop(){
        SceneManager.LoadScene(2);
    }

    public void openOptions(){
        menu.SetActive(false);
        optionsMenu.SetActive(true);
        setText();

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(optionsButton);
        EventSystem.current.SetSelectedGameObject(optionsButton, new BaseEventData(EventSystem.current));
    }

    public void closeOptions(){
        FileManager.loadOptions();
        optionsMenu.SetActive(false);
        menu.SetActive(true);

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(menuButton);
        EventSystem.current.SetSelectedGameObject(menuButton, new BaseEventData(EventSystem.current));
    }
    public void saveOptions(){
        FileManager.saveOptions(gameOptions);

        optionsMenu.SetActive(false);
        menu.SetActive(true);

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(menuButton);
        EventSystem.current.SetSelectedGameObject(menuButton, new BaseEventData(EventSystem.current));
    }

    public void setArcade(){
        gameOptions.mode = gameMode.arcade;
        play();
    }

    public void setVersus(){
        gameOptions.mode = gameMode.versus;
        play();
    }

    public void setOnePlayer(){
        gameOptions.playerNum = 1;
        playersText.text = "1";
    }
    public void setTwoPlayers(){
        gameOptions.playerNum = 2;
        playersText.text = "2";
    }
    public void setThreePlayers(){
        gameOptions.playerNum = 3;
        playersText.text = "3";
    }
    public void setFourPlayers(){
        gameOptions.playerNum = 4;
        playersText.text = "4";
    }

    public void increasePowerUpTime(){
        gameOptions.timeToSpawnPowerUps += 0.5f;
        timeToSpawnPowerUpsText.text = gameOptions.timeToSpawnPowerUps.ToString();
    }
    public void decresePowerUpTime(){
        gameOptions.timeToSpawnPowerUps += -0.5f;
        timeToSpawnPowerUpsText.text = gameOptions.timeToSpawnPowerUps.ToString();
    }
    public void increasePowerUpTimeUltra(){
        gameOptions.timeToSpawnPowerUps += 5;
        timeToSpawnPowerUpsText.text = gameOptions.timeToSpawnPowerUps.ToString();
    }
    public void decresePowerUpTimeUltra(){
        gameOptions.timeToSpawnPowerUps += -5;
        timeToSpawnPowerUpsText.text = gameOptions.timeToSpawnPowerUps.ToString();
    }
    public void increaseMaxPowerUp(){
        gameOptions.maxPowerUps += 1;
        maxPowerUpsText.text = gameOptions.maxPowerUps.ToString();
    }
    public void decreseMaxPowerUp(){
        gameOptions.maxPowerUps += -1;
        maxPowerUpsText.text = gameOptions.maxPowerUps.ToString();
    }

    public void increaseArcadeSpawnTime(){
        gameOptions.timeToIncreaseEnemies += 0.5f;
        arcadeSpawnTimeText.text = gameOptions.timeToIncreaseEnemies.ToString();
    }
    public void decreseArcadeSpawnTime(){
        gameOptions.timeToIncreaseEnemies += -0.5f;
        arcadeSpawnTimeText.text = gameOptions.timeToIncreaseEnemies.ToString();
    }
    public void increaseArcadeSpawnTimeUltra(){
        gameOptions.timeToIncreaseEnemies += 5;
        arcadeSpawnTimeText.text = gameOptions.timeToIncreaseEnemies.ToString();
    }
    public void decreseArcadeSpawnTimeUltra(){
        gameOptions.timeToIncreaseEnemies += -5;
        arcadeSpawnTimeText.text = gameOptions.timeToIncreaseEnemies.ToString();
    }

    public void increaseVersusTime(){
        gameOptions.playTime += 0.5f;
        versusTimeText.text = gameOptions.playTime.ToString();
    }
    public void increaseVersusTimeUltra(){
        gameOptions.playTime += 5;
        versusTimeText.text = gameOptions.playTime.ToString();
    }
    public void decreaseVersusTime(){
        gameOptions.playTime += -0.5f;
        versusTimeText.text = gameOptions.playTime.ToString();
    }
    public void decreaseVersusTimeUltra(){
        gameOptions.playTime += -5;
        versusTimeText.text = gameOptions.playTime.ToString();
    }

    public void increaseVersusLifes(){
        gameOptions.maxLives += 1;
        versusLifesText.text = gameOptions.maxLives.ToString();
    }
    public void decreaseVersusLifes(){
        gameOptions.maxLives += -1;
        versusLifesText.text = gameOptions.maxLives.ToString();
    }

    public void setText(){
        playersText.text = gameOptions.playerNum.ToString();
        timeToSpawnPowerUpsText.text = gameOptions.timeToSpawnPowerUps.ToString();
        maxPowerUpsText.text = gameOptions.maxPowerUps.ToString();
        arcadeSpawnTimeText.text = gameOptions.timeToIncreaseEnemies.ToString();
        versusTimeText.text = gameOptions.playTime.ToString();
        versusLifesText.text = gameOptions.maxLives.ToString();
    }
}
