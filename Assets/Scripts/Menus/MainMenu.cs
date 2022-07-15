using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    

    public void playGame(){
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
}
