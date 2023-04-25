using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class BarracksMenu : HubSubMenu
{
    public CharacterScriptableObjectList characters;
    public GameObject image;
    public TMP_Text equiped;
    public int selectedPlayer = 0;
    public int current = 0;
    public TMP_Text charName;
    public TMP_Text desc;
    public GameObject[] selectedObject;


    public void next(){
        if (current + 1 > menu.data.unlockedCharacters.Length - 1){
            current = 0;
        } else {
            current ++;
            if (!menu.data.unlockedCharacters[current]){
                next();
            }
        }
        equiped.gameObject.SetActive(false);
        loadCharacter();
    }

    public void previous(){
        if (current - 1 < 0){
            current = menu.data.unlockedCharacters.Length - 1;
            if (!menu.data.unlockedCharacters[current]){
                previous();
            }
        } else {
            current += -1;
            if (!menu.data.unlockedCharacters[current]){
                previous();
            }
        }
        equiped.gameObject.SetActive(false);
        loadCharacter();
    }

    public void loadCharacter(){
        image.GetComponent<Image>().sprite = characters.characters[current].image;
        charName.text = characters.characters[current].name;
        desc.text = characters.characters[current].desc[(int)menu.gameOptions.language];
    }

    public void equip(){
        menu.data.selectedChar[selectedPlayer] = current;
        equiped.gameObject.SetActive(true);
        equiped.text = textsScriptableObject.texts[8].textLanguages[(int)menu.gameOptions.language] + " " + textsScriptableObject.texts[0+selectedPlayer].textLanguages[(int)menu.gameOptions.language];
        menu.saveOptionsAndData();
    }

    public void equipDifferent(){
        if(menu.data.selectedChar[0] != current && menu.data.selectedChar[1] != current && menu.data.selectedChar[2] != current && menu.data.selectedChar[3] != current){
            menu.data.selectedChar[selectedPlayer] = current;
            equiped.gameObject.SetActive(true);
            equiped.text = textsScriptableObject.texts[8].textLanguages[(int)menu.gameOptions.language] + " " + textsScriptableObject.texts[0+selectedPlayer].textLanguages[(int)menu.gameOptions.language];
            menu.saveOptionsAndData();
        } else {
            equiped.gameObject.SetActive(true);
            equiped.text= textsScriptableObject.texts[textsScriptableObject.texts.Length - 1].textLanguages[(int)menu.gameOptions.language];
        }
        
    }

    public void selectPlayer(int i){
        
        selectedObject[selectedPlayer].SetActive(false);
        selectedPlayer = i;
        selectedObject[selectedPlayer].SetActive(true);
        current = menu.data.selectedChar[selectedPlayer];
        loadCharacter();
    }

    public void back(){
        menu.thisMenu.transform.position += new Vector3(2500,0,0);
        menu.selectButton(menu.mainButton);
        gameObject.SetActive(false);
    }

    public override void extraStart()
    {
        base.extraStart();
        equiped.gameObject.SetActive(false);
        selectedPlayer = 0;
        current = menu.data.selectedChar[selectedPlayer];
        selectedObject[selectedPlayer].SetActive(true);
        loadCharacter();
    }
}
