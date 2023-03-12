using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : Menu
{
    public GameObject optionsMenu;
    public GameObject optionsButton;
    public GameObject menu;
    public GameObject menuButton;
    public GameObject playMenu;
    public GameObject playButton;

    public GameObject BackgroundImage;
    public GameObject BackgroundImageBlurred;



    void Start() {
        StartMenu();
        Time.timeScale = 1;
    }

    public void openMainHub(){
        try{
            if (rogueliteSave.loadMap){
                SceneManager.LoadScene(3);
            } else {
                SceneManager.LoadScene(2);
            }
        } catch(System.Exception e){
            Debug.Log(e);
            SceneManager.LoadScene(2);
        }
        
        
    }

    public void playGame(){
        BackgroundImage.SetActive(false);
        BackgroundImageBlurred.SetActive(true);
        playMenu.SetActive(true);
        menu.SetActive(false);

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(playButton);
        EventSystem.current.SetSelectedGameObject(playButton, new BaseEventData(EventSystem.current));
        
    }

    public void closePlayMenu(){
        BackgroundImage.SetActive(true);
        BackgroundImageBlurred.SetActive(false);
        playMenu.SetActive(false);
        menu.SetActive(true);

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(menuButton);
        EventSystem.current.SetSelectedGameObject(menuButton, new BaseEventData(EventSystem.current));
    }

    public void play(){
        saveOptionsAndData();
        if(gameOptions.mode == gameMode.roguelite){
            SceneManager.LoadScene(3);
        } else {
            SceneManager.LoadScene(1);
        }
        
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
        BackgroundImage.SetActive(false);
        BackgroundImageBlurred.SetActive(true);

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(optionsButton);
        EventSystem.current.SetSelectedGameObject(optionsButton, new BaseEventData(EventSystem.current));
    }

    public void closeOptions(){
        BackgroundImage.SetActive(true);
        BackgroundImageBlurred.SetActive(false);
        gameOptions = FileManager.loadOptions();
        optionsMenu.SetActive(false);
        menu.SetActive(true);

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(menuButton);
        EventSystem.current.SetSelectedGameObject(menuButton, new BaseEventData(EventSystem.current));
    }
    public void saveOptions(){
        saveOptionsAndData();

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

    public void setRoguelite(){
        gameOptions.mode = gameMode.roguelite;
        rogueliteSave = new RogueliteSave();
        play();
    }

    public void loadRoguelite(){
        gameOptions.mode = gameMode.roguelite;
        if(rogueliteSave.loadMap != true){
            rogueliteSave = new RogueliteSave();
        }
        play();
    }

}
