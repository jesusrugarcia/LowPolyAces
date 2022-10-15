using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossShieldManager : MonoBehaviour
{
    public PlaneManager plane;
    void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<TeamManager>().team != plane.teamManager.team){
            Destroy(other.gameObject);
        }
    }
}
