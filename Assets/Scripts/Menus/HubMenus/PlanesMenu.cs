using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlanesMenu : HubSubMenu
{
    public HubSubMenu hangarMenu;

    public PlanesListScriptableObject planes;

    public int currentPlane = 0;
    public TMP_Text[] statTexts;
    public TMP_Text priceText;
    public TMP_Text modelText;
    public GameObject equippedText;
    public GameObject image;
    public GameObject loadedPlane;
    public GameObject[] equipButtons;
    public GameObject buyButton;
    public GameObject money;
    public TMP_Text moneyText;
    public float moneyRotation = 1;
    public float planeRotation = 1;
    public GameObject thisMenu;
    public float scaleMultiplier;

    public override void extraStart() {
        currentPlane = menu.data.selectedPlayer[0];
        money.SetActive(true);
        money.AddComponent<PropellerRotation>().rotationSpeedX = 0;
        money.GetComponent<PropellerRotation>().rotationSpeedY = moneyRotation;
        moneyText.text = menu.data.points.ToString();
        loadPlane();
    }

    public void loadPlane(){
        try{
            Destroy(loadedPlane.gameObject);
        } catch (System.Exception e){
            Debug.Log(e);
        }
        
        statTexts[0].text = planes.planes[currentPlane].stats.maxHealth.ToString();
        statTexts[1].text = planes.planes[currentPlane].stats.speed.ToString();
        statTexts[2].text = planes.planes[currentPlane].stats.rotationSpeed.ToString();
        statTexts[3].text = planes.planes[currentPlane].stats.acceleration.ToString();
        statTexts[4].text = planes.planes[currentPlane].stats.bulletDamage.ToString();
        statTexts[5].text = planes.planes[currentPlane].stats.shootSpeed.ToString();
        statTexts[6].text = planes.planes[currentPlane].stats.magazineSize.ToString();
        statTexts[7].text = planes.planes[currentPlane].stats.specialAmmo.ToString();
        statTexts[8].text = planes.planes[currentPlane].stats.defenseAmmo.ToString();
        statTexts[9].text = planes.planes[currentPlane].stats.extraBullets.ToString();
        statTexts[10].text = planes.planes[currentPlane].stats.maxMines.ToString();
        statTexts[11].text = planes.planes[currentPlane].stats.maxDrones.ToString();
        statTexts[12].text = planes.planes[currentPlane].stats.evasion.ToString();
        //image.GetComponent<Image>().sprite = planes.planes[currentPlane].image;
        loadedPlane = Instantiate(planes.planes[currentPlane].plane, new Vector3(0,0,0), Quaternion.Euler(-90,0,75),thisMenu.transform);
        loadedPlane.transform.localScale= loadedPlane.transform.localScale * scaleMultiplier;
        loadedPlane.AddComponent<PropellerRotation>().rotationSpeedX = 0;
        loadedPlane.GetComponent<PropellerRotation>().rotationSpeedZ = planeRotation;
        modelText.text = planes.planes[currentPlane].model;
        equippedText.SetActive(false);
        if(menu.data.purchasedPlanes[currentPlane]){
            priceText.text = "";
            setButtonsActive(true);
            buyButton.SetActive(false);
        } else {
            priceText.text = "PRICE: " + planes.planes[currentPlane].stats.price;
            setButtonsActive(false);
            buyButton.SetActive(true);
        }
    }

    public void next(int increase = 1){
        if (currentPlane + increase >= menu.data.unlockedPlanes.Length){
            currentPlane = 0;
            loadPlane();
        } else if(menu.data.unlockedPlanes[currentPlane + increase]){
            currentPlane = currentPlane + increase;
            loadPlane();
        } else {
            next(increase + 1);
        }
    }

    public void buy(){
        if (!menu.data.purchasedPlanes[currentPlane] && menu.data.points >= planes.planes[currentPlane].stats.price){
            menu.data.points += -planes.planes[currentPlane].stats.price;
            menu.data.purchasedPlanes[currentPlane] = true;
            priceText.text = "";
            equippedText.SetActive(false);
            menu.saveOptionsAndData();
            moneyText.text = menu.data.points.ToString();
            setButtonsActive(true);
            selectButton(equipButtons[0]);
            buyButton.SetActive(false);
        }
    }

    public void previous(int increase = 1){
        if (currentPlane - increase < 0){
            currentPlane = menu.data.unlockedPlanes.Length - 1;
            loadPlane();
        } else if(menu.data.unlockedPlanes[currentPlane - increase]){
            currentPlane = currentPlane - increase;
            loadPlane();
        } else {
            previous(increase - 1);
        }
    }

    public void back(){
        hangarMenu.transform.position += new Vector3(2500,0,0);
        hangarMenu.selectButton(hangarMenu.mainButton);
        Destroy(loadedPlane);
        money.SetActive(false);
        gameObject.SetActive(false);
    }

    public void equip(int player){
        menu.data.selectedPlayer[player] = currentPlane;
        equippedText.SetActive(true);
        menu.saveOptionsAndData();

    }

    public void setButtonsActive(bool active){
        for (int i=0; i< equipButtons.Length;i++){
            equipButtons[i].SetActive(active);
        }
    }
}
