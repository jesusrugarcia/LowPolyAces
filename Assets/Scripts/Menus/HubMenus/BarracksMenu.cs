using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BarracksMenu : HubSubMenu
{
    public GameObject[] selectedObject;
    public TMP_Text equiped;
    public int selectedPlayer = 0;
    public int current = 0;

    public void next(){
        if (current + 1 > menu.data.unlockedCharacters.Length - 1){
            current = 0;
        } else {
            current ++;
        }
        loadCharacter();
        equiped.gameObject.SetActive(false);
        selectPlayer(selectedPlayer);
    }

    public void previous(){
        if (current - 1 < 0){
            current = menu.data.unlockedCharacters.Length - 1;
        } else {
            current += -1;
        }
        loadCharacter();
        equiped.gameObject.SetActive(false);
        selectPlayer(selectedPlayer);
    }

    public void loadCharacter(){

    }

    public void equip(){
        if(menu.data.selectedChar[0] != current && menu.data.selectedChar[1] != current && menu.data.selectedChar[2] != current && menu.data.selectedChar[3] != current){
            menu.data.selectedChar[selectedPlayer] = current;
            equiped.gameObject.SetActive(true);
            equiped.text = textsScriptableObject.texts[8].textLanguages[(int)menu.gameOptions.language] + " " + textsScriptableObject.texts[0+selectedPlayer].textLanguages[(int)menu.gameOptions.language];
            menu.saveOptionsAndData();
        } else {
            equiped.gameObject.SetActive(true);
            equiped.text= textsScriptableObject.texts[textsScriptableObject.texts.Length - 1].textLanguages[(int)menu.gameOptions.language];
        }
        selectPlayer(selectedPlayer);
    }

    public void selectPlayer(int i){
        
        selectedObject[selectedPlayer].SetActive(false);
        selectedPlayer = i;
        selectedObject[selectedPlayer].SetActive(true);
    }

    public void back(){
        menu.thisMenu.transform.position += new Vector3(2500,0,0);
        menu.selectButton(menu.mainButton);
        gameObject.SetActive(false);
    }

    public override void extraStart()
    {
        base.extraStart();
        selectedObject[current].SetActive(true);
        equiped.gameObject.SetActive(false);
        selectedPlayer = 0;
        current = menu.data.selectedChar[selectedPlayer];
        loadCharacter();
    }
}
