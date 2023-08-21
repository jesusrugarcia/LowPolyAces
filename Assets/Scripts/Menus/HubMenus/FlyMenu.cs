using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine;

public class FlyMenu : HubSubMenu{

    public void mission(){
        try{
            if(menu.rogueliteSave.finished){
                menu.rogueliteSave = new RogueliteSave();
            }
            menu.gameOptions.mode = gameMode.roguelite;
            menu.saveOptionsAndData();
            SceneManager.LoadScene(3);
        } catch (System.Exception e){
            Debug.Log(e);
            menu.rogueliteSave = new RogueliteSave();
            menu.gameOptions.mode = gameMode.roguelite;
            menu.saveOptionsAndData();
            SceneManager.LoadScene(3);
        }
        
    }

    public void arcade(){
        menu.gameOptions.mode = gameMode.arcade;
        menu.saveOptionsAndData();
        SceneManager.LoadScene(1);
    }

    public void back(){
        menu.thisMenu.transform.position += new Vector3(2500,0,0);
        menu.selectButton(menu.mainButton);
        gameObject.SetActive(false);
    }
}

