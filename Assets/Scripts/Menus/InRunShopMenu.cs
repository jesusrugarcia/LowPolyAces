using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class InRunShopMenu : MonoBehaviour
{
    public WorldMapManager manager;
    public DialogManager dialogManager;

    public GameObject[] playerMoneyObject;
    public TMP_Text[] playerMoneyText;

    public GameObject[] playerHealthObject;
    public TMP_Text[] playerHealthText;

    public GameObject[] repairCostObject;
    public TMP_Text[] repairCostText;
    public int[] repairCost = {0,0,0,0};
    public TMP_Text[] repairMessage;

    public GameObject[] powerUpObject;
    public TMP_Text[] powerUpCost;
    public TMP_Text[] powerUpMessage;
    public TMP_Text[] powerUpName;
    public GameObject[] powerUpImages;
    public PowerUpScriptableObject[] powerUps;
    public PowerUpListScriptableObject[] raritiesLists;

    public TMP_Text fuelPrice;
    public int fuelCost;

    public float timeSinceLastBuy = 0;
    public float iddleTime = 20;
    public int buyed = 0;
    public int buyNum = 10;

    void Start()
    {
        timeSinceLastBuy = 0;
        buyed = 0;
        for(int i=0; i < manager.gameOptions.playerNum;i++){
            playerMoneyObject[i].SetActive(true);
            playerMoneyText[i].text = manager.rogueliteSave.money[i].ToString();
            playerHealthObject[i].SetActive(true);
            var stats = manager.rogueliteSave.stats[i];
            playerHealthText[i].text = stats.health.ToString() + " / " + stats.maxHealth.ToString();
            repairCostObject[i].SetActive(true);
            repairCost[i] = (int)(stats.maxHealth - stats.health)*3;
            repairCostText[i].text = repairCost[i].ToString();
            powerUpObject[i].SetActive(true);
        }
        fuelPrice.text = fuelCost.ToString();
        checkShopVisited();
        dialogManager.loadText(0);
    }
    public void closeMenu(){
        manager.closeShop();
    }

    void FixedUpdate()
    {
        timeSinceLastBuy += Time.deltaTime;
        if (timeSinceLastBuy >= iddleTime){
            timeSinceLastBuy = 0;
            dialogManager.loadText(4);
            buyed = 0;
        }
    }

    public void checkShopVisited(){
        var currentNode = manager.mapGraph.currentMapNode;
        if (manager.mapGraph.nodes[currentNode].shopVisited){
            loadPoweUps(currentNode);
        } else {
            createPowerUps(currentNode);
        }
    }

    public void loadPoweUps(int currentNode){
        powerUps = manager.mapGraph.nodes[currentNode].powerUps;
        for (int i=0; i< powerUps.Length; i++){
            //Debug.Log("Player: " + i.ToString() + " purchased: " + manager.mapGraph.nodes[currentNode].purchasedItems[i].ToString());
            if (!manager.mapGraph.nodes[currentNode].purchasedItems[i]){
                powerUpName[i].text = powerUps[i].title;
                if(powerUps[i].image != null){
                    powerUpImages[i].GetComponent<Image>().sprite = powerUps[i].image;
                }
                powerUpCost[i].text = ((int)powerUps[i].rarity).ToString();
            } else {
                powerUpObject[i].SetActive(false);
            }
            
        }
    }

    public void createPowerUps(int currentNode){
        var powerUps = PowerUpListGenerator.selectPowerUps(4, raritiesLists, true, manager.rogueliteSave);
        manager.mapGraph.nodes[currentNode].shopVisited = true;
        manager.mapGraph.nodes[currentNode].powerUps = powerUps;
        loadPoweUps(currentNode);
    }

    public void repair(int i){
        repairMessage[i].gameObject.SetActive(true);
        if(manager.rogueliteSave.money[i] >= repairCost[i]){
            manager.rogueliteSave.money[i] += -repairCost[i];
            manager.rogueliteSave.stats[i].health = manager.rogueliteSave.stats[i].maxHealth;
            playerMoneyText[i].text = manager.rogueliteSave.money[i].ToString();
            var stats = manager.rogueliteSave.stats[i];
            playerHealthText[i].text = stats.health.ToString() + " / " + stats.maxHealth.ToString();
            repairMessage[i].text = "Repaired!";
            dialogManager.loadText(1);
            checkBuyed();
        } else {
            repairMessage[i].text = "Not Enough Money!";
            dialogManager.loadText(6);
        }
        
    }

    public void repair0(){
        repair(0);
    }
    public void repair1(){
        repair(1);
    }
    public void repair2(){
        repair(2);
    }
    public void repair3(){
        repair(3);
    }

    public void buyFuel(){
        var money = true;
        for (int i=0; i<manager.gameOptions.playerNum;i++){
            if(manager.rogueliteSave.money[i] < fuelCost){
                money = false;
                break;
            }
        }

        if (money &&  manager.rogueliteSave.fuel < 100){
           for (int i=0; i<manager.gameOptions.playerNum;i++){
            manager.rogueliteSave.money[i] += - fuelCost;
            playerMoneyText[i].text = manager.rogueliteSave.money[i].ToString();
            } 
            manager.rogueliteSave.fuel ++;
            manager.fuel.text = manager.rogueliteSave.fuel.ToString();
            dialogManager.loadText(3);
            checkBuyed();
        } else {
            dialogManager.loadText(6);
        }
    }

    public void buyPowerUp(int i){
        if(manager.rogueliteSave.money[i] >= (int)powerUps[i].rarity){
            powerUpObject[i].SetActive(false);
            powerUpMessage[i].gameObject.SetActive(true);
            manager.mapGraph.nodes[manager.mapGraph.currentMapNode].purchasedItems[i] = true;
            manager.rogueliteSave.money[i] += -(int)powerUps[i].rarity;
            manager.rogueliteSave.pendingPowerUps[i] = powerUps[i];
            playerMoneyText[i].text = manager.rogueliteSave.money[i].ToString();
            powerUpMessage[i].text = "Purchased!";
            dialogManager.loadText(2);
            checkBuyed();
        } else {
            powerUpMessage[i].gameObject.SetActive(true);
            powerUpMessage[i].text = "Not Enough Money!";
            dialogManager.loadText(6);
        }
        
    }

    public void buyPowerUp1(){
        buyPowerUp(0);
    }
    public void buyPowerUp2(){
        buyPowerUp(1);
    }
    public void buyPowerUp3(){
        buyPowerUp(2);
    }
    public void buyPowerUp4(){
        buyPowerUp(3);
    }

    public void checkBuyed(){
        buyed++;
        timeSinceLastBuy = 0;
        if(buyed >= buyNum){
            buyed = 0;
            dialogManager.loadText(5);
        }
    }




}
