using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyList : MonoBehaviour
{
    [SerializeField]
    public PlaneModel[] prefabsEasy;
    [SerializeField]
    public PlaneModel[] prefabsMedium;
    [SerializeField]
    public PlaneModel[] prefabsHard;
    [SerializeField]
    public PlaneModel[] prefabsImposible;
}
