using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradesMenu : HubSubMenu
{
    public HubSubMenu hangarMenu;
    public GameObject money;
    public TMP_Text moneyText;
    public float moneyRotation = 1;
    public Upgrades upgrades;
    public TMP_Text[] buttons;
    public TMP_Text[] upgradeTexts;

    public override void extraStart() {
        if(menu.data.upgrades.costPerIncrease.Length > 10){
            upgrades = menu.data.upgrades;
        }
        money.SetActive(true);
        money.AddComponent<PropellerRotation>().rotationSpeedX = 0;
        money.GetComponent<PropellerRotation>().rotationSpeedY = moneyRotation;
        moneyText.text = menu.data.points.ToString();
        for (int i=0; i< buttons.Length;i++){
            buttons[i].text = textsScriptableObject.texts[textsScriptableObject.texts.Length-1].textLanguages[(int)menu.gameOptions.language] + " " + (upgrades.increase[i] + 1) * upgrades.costPerIncrease[i];
            upgradeTexts[i].text = upgrades.increase[i].ToString() + " / " + upgrades.max[i];
        }
    }

    public void upgrade(int i){
        var cost = (upgrades.increase[i] + 1) * upgrades.costPerIncrease[i];
        if(menu.data.points >= cost && upgrades.increase[i] < upgrades.max[i]){
            menu.data.points += - cost;
            upgrades.increase[i] ++;
            menu.data.upgrades = upgrades;
            buttons[i].text = textsScriptableObject.texts[textsScriptableObject.texts.Length-1].textLanguages[(int)menu.gameOptions.language] + " " + (upgrades.increase[i] + 1) * upgrades.costPerIncrease[i];
            upgradeTexts[i].text = upgrades.increase[i].ToString() + " / " + upgrades.max[i];
            menu.saveOptionsAndData();
            moneyText.text = menu.data.points.ToString();
        }
    }

    public void back(){
        hangarMenu.transform.position += new Vector3(2500,0,0);
        hangarMenu.selectButton(hangarMenu.mainButton);
        money.SetActive(false);
        gameObject.SetActive(false);
    }
    
}
