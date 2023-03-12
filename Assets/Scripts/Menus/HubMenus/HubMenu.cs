using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;

public class HubMenu : Menu
{
    public GameObject thisMenu;

    public HubSubMenu FlyMenu;
    public GameObject FlyButton;

    public HubSubMenu HangarMenu;
    public GameObject HangarButton;
    public HubSubMenu BarracksMenu;
    public GameObject BarracksButton;

    void Start() {
        StartMenu();
        setTextLanguages();
        Time.timeScale = 1;
        selectButton(mainButton);
    }

    

    public void fly(){
        thisMenu.transform.position += new Vector3(-2500,0,0);
        FlyMenu.gameObject.SetActive(true);
        selectButton(FlyMenu.mainButton);
    }

    public void Hangar(){
        thisMenu.transform.position += new Vector3(-2500,0,0);
        HangarMenu.gameObject.SetActive(true);
        selectButton(HangarMenu.mainButton);
    }

    public void Barracks(){
        thisMenu.transform.position += new Vector3(-2500,0,0);
        BarracksMenu.gameObject.SetActive(true);
        BarracksMenu.extraStart();
        selectButton(BarracksMenu.mainButton);
    }

    public void quit(){
        SceneManager.LoadScene(0);
    }
}
