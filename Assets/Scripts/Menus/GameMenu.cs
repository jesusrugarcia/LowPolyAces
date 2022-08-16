using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class GameMenu : MonoBehaviour
{
    public bool isPaused = false;
    public GameObject pauseMenu;
    public GameObject pauseButton;

    public void mainMenu(){
        SceneManager.LoadScene(0);
    }
    public void playGame(){
        SceneManager.LoadScene(1);
    }
    public void shop(){
        SceneManager.LoadScene(2);
    }
    public void pause(){
        isPaused = true;
        pauseMenu.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(pauseButton);
        EventSystem.current.SetSelectedGameObject(pauseButton, new BaseEventData(EventSystem.current));
        Time.timeScale = 0f;
    }
    public void resume(){
        isPaused = false;
        pauseMenu.SetActive(false);
        //pauseButton.SetActive(true);
        Time.timeScale = 1f;
    }
}
