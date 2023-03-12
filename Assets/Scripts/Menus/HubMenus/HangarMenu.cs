using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HangarMenu : HubSubMenu
{
    public GameObject thisMenu;
    public HubSubMenu planeMenu;
    public HubSubMenu upgradesMenu;

    public void back(){
        menu.thisMenu.transform.position += new Vector3(2500,0,0);
        menu.selectButton(menu.mainButton);
        gameObject.SetActive(false);
    }

    public void planes(){
        thisMenu.transform.position += new Vector3(-2500,0,0);
        planeMenu.gameObject.SetActive(true);
        planeMenu.extraStart();
        selectButton(planeMenu.mainButton);
    }

    public void upgrades(){
        thisMenu.transform.position += new Vector3(-2500,0,0);
        upgradesMenu.gameObject.SetActive(true);
        upgradesMenu.extraStart();
        selectButton(upgradesMenu.mainButton);
    }
}
