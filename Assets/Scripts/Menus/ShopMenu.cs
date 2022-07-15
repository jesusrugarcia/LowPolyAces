using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ShopMenu : MonoBehaviour
{
    [SerializeField]
    public PlaneModel[] planes;
    public GameObject loadedPlane;
    public PlaneStatsObject stats;

    public SaveData data;
    public FileManager saveManager = new FileManager();

    public GameObject equipButton;
    public GameObject buyButton;
    public GameObject notEnoughPoints;
    public GameObject equiped;

    public Text health;
    public Text speed;
    public Text rotation;
    public Text acceleration;
    public Text shootSpeed;
    public Text price;
    public Text points;

    private void Start() {
        data = saveManager.loadData(planes.Length);
        loadPlane();
        points.text = "Points: " + data.points.ToString("0");
    }

    public void mainMenu(){
        SceneManager.LoadScene(0);
    }

    public void play(){
        SceneManager.LoadScene(1);
    }

    public void loadPlane(){
        loadedPlane = Instantiate(planes[data.selectedPlayer[0]].plane, new Vector3(0,0,0), Quaternion.Euler(-90,0,75));
        Destroy(loadedPlane.GetComponent<PlaneMovement>());
        Destroy(loadedPlane.GetComponent<PlaneShooter>());
        stats = planes[data.selectedPlayer[0]].stats;
        equiped.SetActive(false);
        loadStats();

        if (data.unlockedPlanes[data.selectedPlayer[0]]){
            equipButton.SetActive(true);
            buyButton.SetActive(false);
            price.gameObject.SetActive(false);
        } else {
            equipButton.SetActive(false);
            buyButton.SetActive(true);
            price.gameObject.SetActive(true);
        }
    }

    public void loadStats(){
        health.text = "Health: " + (stats.maxHealth+5).ToString();
        speed.text = "Speed: " + stats.maxSpeed.ToString();
        rotation.text = "Rotation Speed: " + stats.rotationSpeed.ToString();
        acceleration.text = "Acceleration: " + stats.acceleration.ToString();
        shootSpeed.text = "Shoot Speed: " + (1 / stats.shootSpeed).ToString();
        price.text = "Price: " + stats.price;
    }

    public void nextPlane(){
        Destroy(loadedPlane.gameObject);
        data.selectedPlayer[0] ++;
        if (data.selectedPlayer[0] >= planes.Length){
            data.selectedPlayer[0] = 0;
        }
        loadPlane();
        notEnoughPoints.SetActive(false);
    }

    public void previousPlane(){
        Destroy(loadedPlane.gameObject);
        data.selectedPlayer[0] --;
        if (data.selectedPlayer[0] < 0){
            data.selectedPlayer[0] = planes.Length - 1;
        }
        loadPlane();
        notEnoughPoints.SetActive(false);
    }

    public void buy(){
        if (data.points >= stats.price){
            data.points -= stats.price;
            data.unlockedPlanes[data.selectedPlayer[0]] = true;
            points.text = "Points: " + data.points.ToString("0");
            notEnoughPoints.SetActive(false);
            equiped.SetActive(true);
            buyButton.SetActive(false);
            equipButton.SetActive(true);
            saveManager.saveData(data);
        } else {
            notEnoughPoints.SetActive(true);
        }
    }

    public void equip(){
        
        saveManager.saveData(data);
        equiped.SetActive(true);
    }

    public void equip2(){
        data.selectedPlayer[1] = data.selectedPlayer[0];
        equip();
    }

    public void equip3(){
        data.selectedPlayer[2] = data.selectedPlayer[0];
        equip();
    }

    public void equip4(){
        data.selectedPlayer[3] = data.selectedPlayer[0];
        equip();
    }
}
