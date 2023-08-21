using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class WorldMapManager : MonoBehaviour
{
    public WorldMapGraph mapGraph;
    public WorldMapPainter mapPainter;
    public GameObject[] lines;
    public GameObject[] nodes;
    public GameObject[] decoIslands;
    public int decoIslandSize;

    public Vector3 planePosCorrection;

    public PlanesListScriptableObject planes;
    public CharacterScriptableObjectList characters;
    public PowerUpListScriptableObject[] powerUps;
    public GameObject plane;
    public WorldMapMovement mapMovement;

    public SaveData saveData;
    public RogueliteSave rogueliteSave;
    public GameOptions gameOptions;

    float timer = 0;

    public TMP_Text fuel;
    public GameObject turnsWithoutFuelObject;
    public TMP_Text turnsWithoutFuel;

    public bool isPaused = false;
    public GameObject pauseMenu;
    public GameObject pauseButton;

    public GameObject shopMenu;
    public GameObject shopButton;
    // Start is called before the first frame update
    void Start()
    {
        rogueliteSave = FileManager.loadRoguelite();
        saveData = FileManager.loadData(planes,characters, powerUps);
        gameOptions = FileManager.loadOptions();
        paintMap();
        
        fuel.text = rogueliteSave.fuel.ToString();
        if(rogueliteSave.turnsWithoutFuel > 0){
            turnsWithoutFuelObject.SetActive(true);
            turnsWithoutFuel.text = rogueliteSave.turnsWithoutFuel.ToString();
        }
        spawnPlane();
        Time.timeScale = 1f;
        
    }

    void FixedUpdate()
    {
        timer += Time.deltaTime;
    }

    public void paintMap(){
        if(!rogueliteSave.loadMap){
            initMap();
            
            
        } else {
            mapGraph = FileManager.loadMap();
            if ( mapGraph != null){
                 mapPainter.drawMap();
            } else {
                 initMap();
            }
        }
        
    }

    public void initMap(){
        mapPainter.generateAndDrawMap();
        if(rogueliteSave.stage != 0){
            rogueliteSave.fuel += + 3;
            rogueliteSave.turnsWithoutFuel = 0;
        } else {
            rogueliteSave.fuel += mapGraph.layers + 3;
        }
    }

    public void spawnPlane(){
        plane = Instantiate(planes.planes[saveData.selectedPlayer[0]].plane, mapGraph.nodes[mapGraph.currentMapNode].screenPos + planePosCorrection, Quaternion.Euler(-90,0,0));
        mapMovement = plane.AddComponent<WorldMapMovement>();
        plane.transform.localScale *= 1500; //new Vector3(100,100,100);
        mapMovement.mapManager = this;
        mapMovement.plane = plane;
    }

    public void checkNodeAction(int node){
        checkFuel();
        if((mapGraph.nodes[node].type == NodeType.Combat || mapGraph.nodes[node].type == NodeType.Boss) && !mapGraph.nodes[node].combatEnded){
            loadCombat(node);
        } else if(mapGraph.nodes[node].type == NodeType.Shop){
            loadShop(node);
        }
    FileManager.saveRoguelite(rogueliteSave);
    }

    public void loadCombat(int node){
        mapGraph.nodes[mapGraph.currentMapNode].combatEnded = true;
        FileManager.saveMap(mapGraph);
        rogueliteSave.loadMap = true;
        rogueliteSave.seed = mapGraph.nodes[mapGraph.currentMapNode].seed;
        rogueliteSave.enemyCount = mapGraph.nodes[mapGraph.currentMapNode].enemyCount;
        //rogueliteSave.stage = mapGraph.stage;
        if(mapGraph.nodes[node].type == NodeType.Boss){
            rogueliteSave.boss = true;
        } else {
            rogueliteSave.boss = false;
        }
        var opt = FileManager.loadOptions();
        opt.mode = gameMode.roguelite;
        FileManager.saveOptions(opt);
        SceneManager.LoadScene(1);
    }

    public void loadShop(int node){
        isPaused = true;
        //load shop items

        shopMenu.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(shopButton);
        EventSystem.current.SetSelectedGameObject(shopButton, new BaseEventData(EventSystem.current));
    }

    public void closeShop(){
        shopMenu.SetActive(false);
        isPaused = false;
        //FileManager.saveMap(mapGraph);
        //FileManager.saveRoguelite(rogueliteSave);
    }

    public void checkFuel(){
        rogueliteSave.fuel += -1;
        if (rogueliteSave.fuel < 1){
            rogueliteSave.fuel = 0;
            rogueliteSave.turnsWithoutFuel++;
            turnsWithoutFuelObject.SetActive(true);
            turnsWithoutFuel.text = rogueliteSave.turnsWithoutFuel.ToString();
            for (int i=0; i < rogueliteSave.stats.Length;i++){
                rogueliteSave.stats[i].health += - rogueliteSave.turnsWithoutFuel;
            }
        } else {
            rogueliteSave.turnsWithoutFuel = 0;
            turnsWithoutFuelObject.SetActive(false);
        }
        fuel.text = rogueliteSave.fuel.ToString();
    }

    public void pause(){
        isPaused = true;
        pauseMenu.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(pauseButton);
        EventSystem.current.SetSelectedGameObject(pauseButton, new BaseEventData(EventSystem.current));
        //Time.timeScale = 0f;
    }
    public void resume(){
        isPaused = false;
        pauseMenu.SetActive(false);
        //pauseButton.SetActive(true);
        //Time.timeScale = 1f;
    }

    public void mainMenu(){
        SceneManager.LoadScene(0);
    }
}
