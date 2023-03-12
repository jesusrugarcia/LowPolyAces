using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    public InRunShopMenu shop;
    public DialogScriptableObject dialog;
    public GameObject image;
    public TMP_Text charName;
    public TMP_Text text;

    public void loadText(int number){
        image.GetComponent<Image>().sprite = dialog.dialogList[number].image;
        charName.text = dialog.dialogList[number].charName;
        text.text = dialog.dialogList[number].text.textLanguages[(int)shop.manager.gameOptions.language];
    }
}
