using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Rarities{
    common = 100,
    rare = 250
}

[CreateAssetMenu(menuName = "Proyecto Aviones/PowerUp")]
public class PowerUpScriptableObject : ScriptableObject
{
    public PowerUps type;
    public string title;
    public Sprite image; //380 x 300;
    public string desc;
    public Rarities rarity;

}
