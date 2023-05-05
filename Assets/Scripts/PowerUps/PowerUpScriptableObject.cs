using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Rarities{
    common,
    rare,
    epic,
    legendary
}

[CreateAssetMenu(menuName = "Proyecto Aviones/PowerUp")]
public class PowerUpScriptableObject : ScriptableObject
{
    public PowerUps type;
    public string title;
    public Sprite image; //380 x 300;
    public string[] desc;
    public string[] unlock;
    public Rarities rarity;
    public bool initialUnlock = true;

}
