using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Proyecto Aviones/planesObject")]
public class PlaneScriptableObject : ScriptableObject
{
    public GameObject plane;
    public PlaneStatsObject stats;
}
