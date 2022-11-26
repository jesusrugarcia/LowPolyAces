using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonManagerMenu : MonoBehaviour , ISelectHandler, IDeselectHandler
{
    public GameObject selectedObject;
    public bool pointing = false;
    // Start is called before the first frame update

    

    void Update()
{
    if (EventSystem.current.currentSelectedGameObject == this.gameObject)
    {
        selectedObject.SetActive(true);
    } else {
        selectedObject.SetActive(false);
    }
}

    public void OnSelect(BaseEventData eventData)
    {
         selectedObject.SetActive(true);
    }
    
    public void OnDeselect(BaseEventData eventData)
    {
        selectedObject.SetActive(false);
    }

}
