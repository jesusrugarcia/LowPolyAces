using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        pauseButton.SetActive(false);
        Time.timeScale = 0f;
    }
    public void resume(){
        isPaused = false;
        pauseMenu.SetActive(false);
        pauseButton.SetActive(true);
        Time.timeScale = 1f;
    }
}
