using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Proyecto Aviones/CharacterList")]
public class CharacterScriptableObjectList : ScriptableObject
{
    public CharacterScriptableObject[] characters;
}
