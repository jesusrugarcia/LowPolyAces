using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Proyecto Aviones/Character")]
public class CharacterScriptableObject : ScriptableObject
{
    public string charName;
    public Sprite image;
    public int[] increase;
    public string[] desc;
    public PowerUps[] powerUps;
}
