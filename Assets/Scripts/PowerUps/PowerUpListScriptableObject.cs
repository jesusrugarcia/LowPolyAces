using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Proyecto Aviones/PowerUpList")]
public class PowerUpListScriptableObject : ScriptableObject
{
    public PowerUpScriptableObject[] powerUps;
}
