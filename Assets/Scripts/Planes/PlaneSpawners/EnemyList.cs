using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Proyecto Aviones/EnemyList")]
public class EnemyList : ScriptableObject
{
    [SerializeField]
    public PlanesListScriptableObject prefabsEasy;
    [SerializeField]
    public PlanesListScriptableObject prefabsMedium;
    [SerializeField]
    public PlanesListScriptableObject prefabsHard;
    [SerializeField]
    public PlanesListScriptableObject prefabsImposible;
    [SerializeField]
    public PlanesListScriptableObject prefabsBoss;
}

