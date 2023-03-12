using System;
using UnityEngine;

public enum Languages{
    English,
    Español
}

[Serializable]
public class DialogText{
    public string[] textLanguages;
}

[Serializable]
public class Dialog{
    public string descr;
    public DialogText text;
    public string[] responseOptions;
    public string charName;
    public Sprite image;
}

[CreateAssetMenu(menuName = "Proyecto Aviones/Dialog")]
public class DialogScriptableObject : ScriptableObject
{
    public Dialog[] dialogList;
}
