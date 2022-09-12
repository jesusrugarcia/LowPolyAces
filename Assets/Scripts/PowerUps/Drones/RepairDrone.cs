using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairDrone : MonoBehaviour
{
    public PlaneManager plane;
    public float timer = 0;

    private void FixedUpdate() {
        timer += Time.deltaTime;
        if(timer >= plane.stats.auxDroneSpeed && plane.stats.health < plane.stats.maxHealth){
            timer = 0;
            plane.stats.health ++;
        }
    }
}
