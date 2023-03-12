using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class HubSubMenu : MonoBehaviour
{
    public TMP_Text[] texts;
    public MenuTextScriptableObject textsScriptableObject;
    public GameObject mainButton;
    public HubMenu menu;

    void Start()
    {
        setTextLanguages();
        selectButton(mainButton);
        extraStart();
    }

    public void selectButton(GameObject button){
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(button);
        EventSystem.current.SetSelectedGameObject(button, new BaseEventData(EventSystem.current));
    }

    public void setTextLanguages(){
        for (int i=0; i < texts.Length; i++){
            texts[i].text = textsScriptableObject.texts[i].textLanguages[(int)menu.gameOptions.language];
        }
    }

    public virtual void extraStart(){

    }
}
