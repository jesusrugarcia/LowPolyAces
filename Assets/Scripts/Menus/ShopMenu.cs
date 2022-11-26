using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ShopMenu : Menu
{
    [SerializeField]
    public PlanesListScriptableObject planes;
    public GameObject loadedPlane;
    public int currentPlane = 0;
    public PlaneStatsObject stats;

    

    public GameObject[] equipButtons;
    public GameObject buyButton;
    public GameObject notEnoughPoints;
    public GameObject equiped;

    public GameObject buttonToPlay;

    public Text health;
    public Text speed;
    public Text rotation;
    public Text acceleration;
    public Text shootSpeed;
    public Text price;
    public Text points;

    

    void Start() {
        planesSize = planes.planes.Length;
        StartMenu();
        currentPlane = data.selectedPlayer[0];
        loadPlane();
        points.text = "Points: " + data.points.ToString();
        currentPlane = data.selectedPlayer[0];
    }

    public void mainMenu(){
        SceneManager.LoadScene(0);
    }

    public void play(){
        if(gameOptions.mode == gameMode.roguelite ){
            if(rogueliteSave.loadMap != true){
                rogueliteSave = new RogueliteSave();
            }
            SceneManager.LoadScene(3);
        } else {
            SceneManager.LoadScene(1);
        }
        
    }

    public void loadPlane(){
        loadedPlane = Instantiate(planes.planes[currentPlane].plane, new Vector3(0,0,0), Quaternion.Euler(-90,0,75));
        //Destroy(loadedPlane.GetComponent<PlaneMovement>());
        //Destroy(loadedPlane.GetComponent<PlaneShooter>());
        stats = planes.planes[currentPlane].stats;
        equiped.SetActive(false);
        loadStats();

        if (data.unlockedPlanes[currentPlane]){
            for(int i= 0; i < equipButtons.Length; i++){
                equipButtons[i].SetActive(true);
            }
            buyButton.SetActive(false);
            price.gameObject.SetActive(false);
        } else {
            for(int i= 0; i < equipButtons.Length; i++){
                equipButtons[i].SetActive(false);
            }
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
        currentPlane ++;
        if (currentPlane >= planes.planes.Length){
            currentPlane = 0;
        }
        loadPlane();
        notEnoughPoints.SetActive(false);
    }

    public void previousPlane(){
        Destroy(loadedPlane.gameObject);
        currentPlane --;
        if (currentPlane < 0){
            currentPlane = planes.planes.Length - 1;
        }
        loadPlane();
        notEnoughPoints.SetActive(false);
    }

    public void buy(){
        if (data.points >= stats.price){
            data.points -= stats.price;
            data.unlockedPlanes[currentPlane] = true;
            points.text = "Points: " + data.points.ToString("0");
            notEnoughPoints.SetActive(false);
            equiped.SetActive(true);
            buyButton.SetActive(false);
            for(int i= 0; i < equipButtons.Length; i++){
                equipButtons[i].SetActive(true);
            }
            saveOptionsAndData();
        } else {
            notEnoughPoints.SetActive(true);
        }
    }

    public void equip(){
        data.selectedPlayer[0] = currentPlane;
        saveSelection();
    }

    public void equip2(){
        data.selectedPlayer[1] = currentPlane;
        saveSelection();
    }

    public void equip3(){
        data.selectedPlayer[2] = currentPlane;
        saveSelection();
    }

    public void equip4(){
        data.selectedPlayer[3] = currentPlane;
        saveSelection();
        
    }
    public void saveSelection(){
        saveOptionsAndData();
        equiped.SetActive(true);
        selectButton();
    }

    public void selectButton(){
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(buttonToPlay);
        EventSystem.current.SetSelectedGameObject(buttonToPlay, new BaseEventData(EventSystem.current));
    }
}
