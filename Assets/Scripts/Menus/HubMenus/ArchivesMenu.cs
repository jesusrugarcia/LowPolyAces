using UnityEngine.UI;
using TMPro;
using UnityEngine;

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
    public TMP_Text desc;
    public GameObject[] selectedObject;

    public int currentList = 0; //0 planes, 1 chars, 2 powerups
    public int powerUpList = 0;

    public GameObject thisMenu;
    public float scaleMultiplier = 1;
    public float planeRotation = 1;
    
    
    public void back(){
        menu.thisMenu.transform.position += new Vector3(2500,0,0);
        menu.selectButton(menu.mainButton);
        gameObject.SetActive(false);
    }

    public override void extraStart()
    {
        base.extraStart();
        clear();
        current = 0;
        currentList = 0;
        selectedObject[currentList].SetActive(true);
        loadPlane();
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
        current ++;
        if (current >= planes.planes.Length){
            current = 0;
        }
        loadPlane();
    }

    public void previousPlane(){
        current += -1;
        if (current < 0 ){
            current = planes.planes.Length;
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
    }

    public void nextChar(){
        current ++;
        if (current >= characters.characters.Length){
            current = 0;
        }
        loadCharacter();
    }

    public void previousChar(){
        current += -1;
        if (current < 0 ){
            current = characters.characters.Length;
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
    }

    public void nextPowerUps(){
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
}
