using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Proyecto Aviones/planesListObject")]
public class PlanesListScriptableObject : ScriptableObject
{
    public PlaneScriptableObject[] planes;
}
