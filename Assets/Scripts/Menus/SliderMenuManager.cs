using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum OptionTypes{
    PlayerNumber,
    TimeToSpawnPowerUp,
    MaxPowerUps,
    TimeToIncreaseEnemies,
    VersusPlayTime,
    VersusLives,
    Volume,
}

public class SliderMenuManager : MonoBehaviour
{
    public GameObject selectedObject;
    public TMP_Text text;
    public Slider slider;
    public Menu menu;
    public float minValue;
    public float maxValue;
    public OptionTypes type;
    public bool text0 = false; //tells if the text printing needs spetial treatment.

    private void Start()
    {
        if(menu.loaded == false){
            menu.loadOptionsAndData();
        }
        slider.maxValue = maxValue;
        slider.minValue = minValue;
        getValue();
    }


    public void OnValueChange(){
        setValue();
    }

    public void getValue(){
        if (type == OptionTypes.PlayerNumber){
            slider.value = (int)menu.gameOptions.playerNum;
        } else if (type == OptionTypes.TimeToSpawnPowerUp){
            slider.value = menu.gameOptions.timeToSpawnPowerUps*2;
            text0 = true;
            text.text = (slider.value*2).ToString();
        } else if (type == OptionTypes.MaxPowerUps){
            slider.value = (int)menu.gameOptions.maxPowerUps;
        } else if (type == OptionTypes.TimeToIncreaseEnemies){
            slider.value = menu.gameOptions.timeToIncreaseEnemies*2;
            text0 = true;
            text.text = (slider.value*2).ToString();
        } else if (type == OptionTypes.VersusPlayTime){
            slider.value = menu.gameOptions.playTime*2;
            text0 = true;
            text.text = (slider.value*2).ToString();
        } else if (type == OptionTypes.VersusLives){
            slider.value = (int)menu.gameOptions.maxLives;
        } else if (type == OptionTypes.Volume){
            slider.value = menu.gameOptions.musicVolume;
        }
    }

    public void setValue(){
        if (type == OptionTypes.PlayerNumber){
            menu.gameOptions.playerNum = (int)slider.value;
        } else if (type == OptionTypes.TimeToSpawnPowerUp){
            menu.gameOptions.timeToSpawnPowerUps = slider.value * 0.5f;
            text.text = (slider.value*0.5f).ToString();
        } else if (type == OptionTypes.MaxPowerUps){
            menu.gameOptions.maxPowerUps = (int)slider.value;
        } else if (type == OptionTypes.TimeToIncreaseEnemies){
            menu.gameOptions.timeToIncreaseEnemies = slider.value * 0.5f;
            text.text = (slider.value*0.5f).ToString();
        } else if (type == OptionTypes.VersusPlayTime){
            menu.gameOptions.playTime = slider.value * 0.5f;
            text.text = (slider.value*0.5f).ToString();
        } else if (type == OptionTypes.VersusLives){
            menu.gameOptions.maxLives = (int)slider.value;
        } else if (type == OptionTypes.Volume){
            menu.gameOptions.musicVolume = slider.value;
            menu.setVolume();
        }
    }

    public void setText(){
        if(!text0){
            text.text = slider.value.ToString("0.##");
        }
    }

    void FixedUpdate()
{
    if (EventSystem.current.currentSelectedGameObject == this.gameObject)
    {
        selectedObject.SetActive(true);
    } else {
        selectedObject.SetActive(false);
    }
    OnValueChange();
    setText();
}
}
