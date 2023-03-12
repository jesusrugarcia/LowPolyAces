using UnityEngine.EventSystems;
using UnityEngine;
using TMPro;

public class Menu : MonoBehaviour
{
    public bool loaded = false;
    public SaveData data;
    public PlanesListScriptableObject planeList;
    public int sizeCharacters = 4;
    public int sizePowerUps = 9;
    public GameOptions gameOptions;
    public RogueliteSave rogueliteSave;

    public AudioManager audioManager;

    public TMP_Text[] texts;
    public MenuTextScriptableObject textsScriptableObject;
    public GameObject mainButton;

    public virtual void StartMenu()
    {
        loadOptionsAndData();
    }

    public void loadOptionsAndData(){
        gameOptions = FileManager.loadOptions();
        data = FileManager.loadData(planeList,sizeCharacters,sizePowerUps);
        rogueliteSave = FileManager.loadRoguelite();
        audioManager.updateVolume();
        loaded = true;
    }

    public virtual void saveOptionsAndData(){
        FileManager.saveData(data);
        FileManager.saveOptions(gameOptions);
        FileManager.saveRoguelite(rogueliteSave);
    }

    public void setVolume(){
        audioManager.updateVolume();
    }

    public void selectButton(GameObject button){
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(button);
        EventSystem.current.SetSelectedGameObject(button, new BaseEventData(EventSystem.current));
    }

    public void setTextLanguages(){
        for (int i=0; i < texts.Length; i++){
            texts[i].text = textsScriptableObject.texts[i].textLanguages[(int)gameOptions.language];
        }
    }
}
