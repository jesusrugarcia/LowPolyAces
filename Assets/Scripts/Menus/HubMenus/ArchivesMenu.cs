using UnityEngine.UI;
using TMPro;
using UnityEngine;
using System;

public class ArchivesMenu : HubSubMenu
{
    public CharacterScriptableObjectList characters;
    public PowerUpListScriptableObject[] powerUps;
    public PlanesListScriptableObject planes;

    public int current = 0;

    public GameObject characterImage;
    public GameObject characterImageBackground;
    public GameObject powerUpImage;
    public GameObject loadedPlane;

    public TMP_Text currentName;
    public TMP_Text newName;
    public TMP_Text desc;
    public GameObject[] selectedObject;
    public GameObject[] newObject;

    public int currentList = 0; //0 planes, 1 chars, 2 powerups
    public int powerUpList = 0;

    public GameObject thisMenu;
    public float scaleMultiplier = 1;
    public float planeRotation = 1;
    
    
    public void back(){
        menu.thisMenu.transform.position += new Vector3(2500,0,0);
        menu.selectButton(menu.mainButton);
        gameObject.SetActive(false);
        menu.saveOptionsAndData();
    }

    public override void extraStart()
    {
        base.extraStart();
        clear();
        selectedObject[currentList].SetActive(false);
        current = 0;
        currentList = 0;
        selectedObject[currentList].SetActive(true);
        loadPlane();
        checkUnlocks();
        newName.gameObject.SetActive(false);
    }

    public void clear(){
        try{
            Destroy(loadedPlane.gameObject);
            characterImage.SetActive(false);
            characterImageBackground.SetActive(false);
            powerUpImage.SetActive(false);
        } catch (System.Exception e){
            Debug.Log(e);
        }
    }

    public void selectCategory(int i){
        clear();
        selectedObject[currentList].SetActive(false);
        currentList = i;
        powerUpList = 0;
        selectedObject[currentList].SetActive(true);
        current = 0;

        if (currentList == 0){
            loadPlane();
        } else if (currentList == 1){
            loadCharacter();
        } else {
            loadPowerUp();
        }
    }

    public void next(){
        if (currentList == 0){
            nextPlane();
        } else if(currentList == 1){
            nextChar();
        } else {
            nextPowerUps();
        }
    }

    public void previous(){
        if (currentList == 0){
            previousPlane();
        } else if(currentList == 1){
            previousChar();
        } else {
            previousPowerUps();
        }
    }

    public void loadPlane(){
        try{
            Destroy(loadedPlane.gameObject);
        } catch (System.Exception e){
            Debug.Log(e);
        }
        if(menu.data.notifyPlanes[current]){
            newName.gameObject.SetActive(true);
        } else {
            newName.gameObject.SetActive(false);
        }
        
        loadedPlane = Instantiate(planes.planes[current].plane, new Vector3(0,0,0), Quaternion.Euler(-90,0,75),thisMenu.transform);
        loadedPlane.transform.localScale= loadedPlane.transform.localScale * scaleMultiplier;
        loadedPlane.AddComponent<PropellerRotation>().rotationSpeedX = 0;
        loadedPlane.GetComponent<PropellerRotation>().rotationSpeedZ = planeRotation;
        currentName.text = planes.planes[current].model;
        if(menu.data.unlockedPlanes[current]){
            desc.text = planes.planes[current].desc[(int)menu.gameOptions.language];
        } else {
            desc.text = planes.planes[current].unlock[(int)menu.gameOptions.language];
        }
    }

    public void nextPlane(){
        try{
            menu.data.notifyPlanes[current] = false;
            checkUnlockedPlanes();
        } catch(Exception e){
            Debug.Log(e);
        }
        
        current ++;
        if (current >= planes.planes.Length){
            current = 0;
        }
        
        loadPlane();
    }

    public void previousPlane(){
         try{
            menu.data.notifyPlanes[current] = false;
            checkUnlockedPlanes();
        } catch(Exception e){
            Debug.Log(e);
        }
        current += -1;
        if (current < 0 ){
            current = planes.planes.Length-1;
        }
        
        loadPlane();
    }

    public void loadCharacter(){
        characterImage.SetActive(true);
        characterImageBackground.SetActive(true);
        characterImage.GetComponent<Image>().sprite = characters.characters[current].image;
        currentName.text = characters.characters[current].charName;

        if(menu.data.unlockedCharacters[current] == true){
            desc.text = characters.characters[current].desc[(int)menu.gameOptions.language];
        } else {
            desc.text = characters.characters[current].unlock[(int)menu.gameOptions.language];
        }

        if(menu.data.notifyCharacters[current]){
            newName.gameObject.SetActive(true);
        } else {
            newName.gameObject.SetActive(false);
        }
    }

    public void nextChar(){
        try{
            menu.data.notifyCharacters[current] = false;
            checkUnlockedCharacters();
        } catch(Exception e){
            Debug.Log(e);
        }
        current ++;
        if (current >= characters.characters.Length){
            current = 0;
        }
        
        loadCharacter();
    }

    public void previousChar(){
        try{
            menu.data.notifyCharacters[current] = false;
            checkUnlockedCharacters();
        } catch(Exception e){
            Debug.Log(e);
        }
        current += -1;
        if (current < 0 ){
            current = characters.characters.Length-1;
        }
        
        loadCharacter();
    }

    public void loadPowerUp(){
        powerUpImage.SetActive(true);
        powerUpImage.GetComponent<Image>().sprite = powerUps[powerUpList].powerUps[current].image;
        currentName.text = powerUps[powerUpList].powerUps[current].title;

        if(menu.data.unlockedPowerUps[powerUpList].powerUps[current] == true){
            desc.text = powerUps[powerUpList].powerUps[current].desc[(int)menu.gameOptions.language];
        } else {
            desc.text = powerUps[powerUpList].powerUps[current].unlock[(int)menu.gameOptions.language];
        }

        if(menu.data.notifyPowerUps[powerUpList].powerUps[current] == true){
            newName.gameObject.SetActive(true);
        } else {
            newName.gameObject.SetActive(false);
        }
    }

    public void nextPowerUps(){
        try{
            menu.data.notifyPowerUps[powerUpList].powerUps[current] = false;
            checkUnlockedPowerUps();
        } catch(Exception e){
            Debug.Log(e);
        }
        
        current ++;
        if (current >= powerUps[powerUpList].powerUps.Length){
            powerUpList ++;
            if (powerUpList >= powerUps.Length){
                powerUpList = 0;
            }
            current = 0;
        }
        
        loadPowerUp();
    }

    public void previousPowerUps(){
        try{
            menu.data.notifyPowerUps[powerUpList].powerUps[current] = false;
            checkUnlockedPowerUps();
        } catch(Exception e){
            Debug.Log(e);
        }
        current += -1 ;
        if (current < 0 ){
            powerUpList +=  - 1;
            if (powerUpList < 0){
                powerUpList = powerUps.Length - 1;
            }
            current = powerUps[powerUpList].powerUps.Length -1;
        }
        
        loadPowerUp();
    }

    public void checkUnlockedPlanes(){
        for (int i = 0; i< menu.data.notifyPlanes.Length; i++){
            if (menu.data.notifyPlanes[i]){
                newObject[0].SetActive(true);
                return;
            }
        }
        newObject[0].SetActive(false);
    }

    public void checkUnlockedCharacters(){
        for (int i = 0; i< menu.data.notifyCharacters.Length; i++){
            if (menu.data.notifyCharacters[i]){
                newObject[1].SetActive(true);
                return;
            }
        }
        newObject[1].SetActive(false);
    }

    public void checkUnlockedPowerUps(){
        for (int i = 0; i< menu.data.notifyPowerUps.Length; i++){
            for(int j=0; j< menu.data.notifyPowerUps[i].powerUps.Length; j++){
                if (menu.data.notifyPowerUps[i].powerUps[j]){
                newObject[2].SetActive(true);
                return;
            }
            }
            
        }
        newObject[2].SetActive(false);
    }

    public void checkUnlocks(){
        checkUnlockedCharacters();
        checkUnlockedPlanes();
        checkUnlockedPowerUps();
    }
}
