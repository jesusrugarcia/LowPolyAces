using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PowerUpMenuManager : MonoBehaviour
{
    public GameController controller;
    public PowerUpListScriptableObject[] raritiesLists;

    public TMP_Text[] powerUpsTitles;
    public TMP_Text[] powerUpsDescs;
    public GameObject[] powerUpsImages;

    public PowerUpScriptableObject[] powerUps;

    public int powerUpNumber = 3;

    public int currentPlayer = 0;
    

    public void selectPowerUps(){
        powerUps = new PowerUpScriptableObject[powerUpNumber];

        for(int i=0; i<powerUpNumber; i++){
            selectPowerUp(i);
        }
    }

    public void selectPowerUp(int i){
        var rarity = UnityEngine.Random.Range(0,raritiesLists.Length);
        powerUps[i] = raritiesLists[rarity].powerUps[UnityEngine.Random.Range(0,raritiesLists[rarity].powerUps.Length)];

        for(int j=0; j<i; j++){
            if( powerUps[j] ==  powerUps[i]
            || ((powerUps[i].type == PowerUps.TurretDrone || powerUps[i].type == PowerUps.RepairDrone || powerUps[i].type == PowerUps.MineDrone || powerUps[i].type == PowerUps.MissileDrone || powerUps[i].type == PowerUps.GunnerDrone || powerUps[i].type == PowerUps.ShieldDrone) && controller.players[currentPlayer].GetComponent<PlaneStats>().drones >= controller.players[currentPlayer].GetComponent<PlaneStats>().maxDrones)
            ){
                selectPowerUp(i);
            }
        }

        powerUpsTitles[i].text = powerUps[i].title;
        powerUpsDescs[i].text = powerUps[i].desc;
        if(powerUps[i].image != null){
            powerUpsImages[i].GetComponent<Image>().sprite = powerUps[i].image;
        }
    }

    public void select1(){
        Debug.Log("Player " + currentPlayer + " health: " + controller.players[currentPlayer].GetComponent<PlaneManager>().stats.health);
        Debug.Log("Power up type: " + powerUps[0].type);
        controller.centralManager.managePowerUp(controller.players[currentPlayer],powerUps[0].type,null);
        LoadMap();
    }

    public void select2(){
        controller.centralManager.managePowerUp(controller.players[currentPlayer],powerUps[1].type,null);
        LoadMap();
    }

    public void select3(){
        controller.centralManager.managePowerUp(controller.players[currentPlayer],powerUps[2].type,null);
        LoadMap();
    }

    public void Repair(){
        controller.centralManager.managePowerUp(controller.players[currentPlayer],PowerUps.Repair,null);
        LoadMap();
    }

    public void LoadMap(){
        controller.rogueliteSave.stats[currentPlayer].copyStats(controller.players[currentPlayer].GetComponent<PlaneStats>());
        FileManager.saveRoguelite(controller.rogueliteSave);
        if(currentPlayer < controller.gameOptions.playerNum-1){
            currentPlayer++;
            selectPowerUps();
        } else {
            SceneManager.LoadScene(3);
        }
        
    }
}