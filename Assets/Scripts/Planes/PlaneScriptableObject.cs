using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Proyecto Aviones/planesObject")]
public class PlaneScriptableObject : ScriptableObject
{
    public string model;
    public GameObject plane;
    //public Sprite image;
    public PlaneStatsObject stats;
    public bool unlockedByDefault;
    public bool purchasedByDefault;
    public string[] desc;
    public string[] unlock;
}
